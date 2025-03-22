using Lab5TestTask.Data;
using Lab5TestTask.Enums;
using Lab5TestTask.Models;
using Lab5TestTask.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Lab5TestTask.Services.Implementations;

/// <summary>
/// SessionService implementation.
/// Implement methods here.
/// </summary>
public class SessionService : ISessionService
{
    private readonly ApplicationDbContext _dbContext;

    public SessionService(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    
        public async Task<Session> GetSessionAsync()
    {
        return await _dbContext.Sessions
            .Where(s => s.DeviceType == DeviceType.Desktop) // Фильтрация только по рабочим столам
            .OrderBy(s => s.StartedAtUTC) // Сортировка по дате начала
            .FirstOrDefaultAsync(); // Получение первого элемента
    }



    public async Task<List<Session>> GetSessionsAsync()
    {
        return await _dbContext.Sessions
            .Where(s => s.User.Status == UserStatus.Active // Предполагаем, что "Active" является индикатором активности
                        && s.EndedAtUTC < new DateTime(2025, 1, 1)) // Сеансы, завершенные до 2025 года
            .ToListAsync(); // Асинхронное получение списка
    }

}
