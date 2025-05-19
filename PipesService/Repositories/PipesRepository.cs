

using System.Data;
using System.Data.Common;
using System.Transactions;
using Microsoft.Extensions.Configuration;
using PipeCommon.Database;
using PipeService.Contracts;
using PipeService.Repositories.Models;

namespace PipeService.Repositories
{
    public class PipesRepository : BaseRepository, IPipesRepository
    {
        public PipesRepository(IConfiguration moduleConfiguration
                                , string connectionStringParamName = "ConnectionString")
                                : base(moduleConfiguration, connectionStringParamName)
        {
        }

        public async Task<int> DeletePipe(int Id, IDbTransaction transaction) => 
        await transaction.ExecuteCustomAsync(
            """
                delete from "pipe_Information".pipes p where p.pipe_id = @pipe_id;
            """, new { pipe_id = Id });

        public async Task<int> DeletePackage(int Id, IDbTransaction transaction) =>
        await transaction.ExecuteCustomAsync(
            """
                delete from "pipe_Information".packages p where p.package_id = @package_id;
            """, new { package_id = Id });

        public async Task<IEnumerable<DbPackageItem>> GetPackages(IDbTransaction transaction) =>
        await transaction.QueryCustomAsync<DbPackageItem>(
            """
             select  
            	p.package_id,
            	p.package_number,
            	p.package_date
            from "pipe_Information".packages p    
            """
        );

        public async Task<IEnumerable<DbPipeItem>> GetPipes(IDbTransaction transaction) =>
        await transaction.QueryCustomAsync<DbPipeItem>(
            """
            SELECT
              p.pipe_id,
              p.pipe_number,
              p.is_good_quality,
              sg.grade_name,
              p.length_meters,
              p.diameter_mm,
              p.weight_kg,
              pk.package_number,
              pk.package_date,
              p.steel_grade_id as GradeId,
              p.package_id as PackageId
            FROM
              "pipe_Information".pipes p
            JOIN
              "pipe_Information".steel_grades sg ON p.steel_grade_id = sg.grade_id
            LEFT JOIN
              "pipe_Information".packages pk ON p.package_id = pk.package_id
            ORDER BY
              p.pipe_id;  
            """
        );

        public async Task<IEnumerable<DbSteelGradeItem>> GetSteelGrades(IDbTransaction transaction) =>
            await transaction.QueryCustomAsync<DbSteelGradeItem>(
            """
            select 
                sg.grade_id,
                sg.grade_name
            from "pipe_Information".steel_grades sg
            """
            );

        public async Task<int> UpdatePipe(UpdatePipeRequest request, IDbTransaction transaction) =>
            await transaction.ExecuteCustomAsync(
            """
            UPDATE "pipe_Information".pipes
             set 
                pipe_number = @pipeNumber
                ,is_good_quality = @isGoodQuality
                ,steel_grade_id = @steelGradeId
                ,length_meters = @lengthMeters
                ,diameter_mm = @diameterMm
                ,weight_kg = @weightKg
                ,package_id = @packageId
             where pipe_id = @pipe_id
            """,new {
                       pipe_id = request.PipeId,
                       pipeNumber = request.PipeNumber,
                       isGoodQuality = request.IsGoodQuality,
                       steelGradeId = request.GradeId,
                       lengthMeters = request.LengthMeters,
                       diameterMm = request.DiameterMm,
                       weightKg = request.WeightKg,
                       packageId = request.PackageId
                     });

        public async Task<int> AddPipe(AddPipeRequest request ,IDbTransaction transaction) =>
            await transaction.ExecuteCustomAsync(
            """
            INSERT INTO "pipe_Information".pipes (
                pipe_number
                ,is_good_quality
                ,steel_grade_id
                ,length_meters
                ,diameter_mm
                ,weight_kg
                ,package_id
            ) VALUES (
              @pipeNumber
              ,@isGoodQuality
              ,@steelGradeId
              ,@lengthMeters
              ,@diameterMm
              ,@weightKg
              ,@packageId
            )
            """, new { pipeNumber = request.PipeNumber,
                       isGoodQuality = request.IsGoodQuality,
                       steelGradeId = request.GradeId,
                       lengthMeters = request.LengthMeters,
                       diameterMm = request.DiameterMm,
                       weightKg = request.WeightKg,
                       packageId = request.PackageId
                     });

        public async Task<int> AddPackage(AddPackageRequest request, IDbTransaction transaction) =>
          await transaction.ExecuteCustomAsync(
          """
            INSERT INTO "pipe_Information".packages
                (package_number, package_date)
            VALUES
                (@PackageNumber, @PackageDate);
            """, new
            {
              PackageNumber = request.PackageNumber,
              PackageDate = request.PackageDate.Date,
          });

        public async Task<int> UpdatePackage(UpdatePackageRequest request, IDbTransaction transaction) =>
            await transaction.ExecuteCustomAsync(
            """
                UPDATE "pipe_Information".packages
                SET 
                    package_number = @PackageNumber,
                    package_date   = @PackageDate
                WHERE package_id = @PackageId;
            """, new
            {
                PackageNumber = request.PackageNumber,
                PackageDate = request.PackageDate.Date,
                PackageId = request.PackageId
            });      
    }
}
