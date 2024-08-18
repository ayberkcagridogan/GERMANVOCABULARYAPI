using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace GermanVocabularyAPI.Exception;

public static class ExceptionHandler
{
    public static async Task<ActionResult> HandleExceptionAsync(System.Exception ex, Func<Task<bool>> entityExists = null)
    {
        switch (ex)
        {
            case DbUpdateException dbEx when dbEx.InnerException is PostgresException postgresEx && postgresEx.SqlState == "23505":
                return new ConflictObjectResult(new { message = "Bu kayıt zaten mevcut. Lütfen farklı bir değer girin." });

            case DbUpdateConcurrencyException:
                if (entityExists != null && !await entityExists())
                {
                    return new NotFoundObjectResult(new { message = "Kayıt bulunamadı." });
                }
                return new ConflictObjectResult(new { message = "Veritabanı güncellemesinde çakışma tespit edildi." });

            case ValidationException validationEx:
                return new BadRequestObjectResult(new { message = "Veri doğrulama hatası: " + validationEx.Message });

            default:
                return new ObjectResult(new { message = "Bir hata oluştu: " + ex.Message }) 
                { 
                    StatusCode = StatusCodes.Status500InternalServerError 
                };
        }
    }
}
