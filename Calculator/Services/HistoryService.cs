using System;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Calculator.Entities;
using Serilog;

namespace Calculator.Services
{
    public class HistoryService
    {
        private readonly DataContext _context;

        public HistoryService(DataContext context)
        {
            _context = context;
        }
        public void LogToDatabase(float a, float b, string op, float? result)
        {
            StringBuilder sb = new StringBuilder();
            // have to put "NaN" because by default it may be "не число"
            sb.AppendFormat("{0} {1} {2} = {3}", a, op, b, result.Equals(float.NaN) ? "NaN" : result.ToString());

            _context.HistoryItems.Add(
                new HistoryItem()
                {
                    Argument1 = a,
                    Argument2 = b,
                    DateTime = DateTime.Now,
                    Operation = op,
                    Result = result.Equals(float.NaN) ? null : result,
                    Expression = sb.ToString()
                }
            );
            _context.SaveChanges();
        }

        public void SeriLogOperation(float a, float b, string op, float result)
        {
            float? resultToLog = null;
            if (result.Equals(float.NaN))
            {
                Log.Information("{@Argument1} {@Operation} {@Argument2} = NaN", a, op, b);
            }
            else
            {
                Log.Information("{@Argument1} {@Operation} {@Argument2} = {@Result}", a, op, b, resultToLog);
            }
        }

        public async Task<IEnumerable<HistoryItem>> SearchDatabaseByExpressionAsync(string expression)
        {
            return await _context.HistoryItems.Where(t => t.Expression.Contains(expression))
                         .ToListAsync();
        }

        public async Task<IEnumerable<HistoryItem>> SearchDatabaseByOperationAsync(string op)
        {
            return await _context.HistoryItems.Where(t => t.Operation == op)
                .ToListAsync<HistoryItem>();
        }

        public async Task<IEnumerable<HistoryItem>> SearchDatabaseAsync(float? result)
        {
            return await _context.HistoryItems.Where(t => t.Result == result).ToListAsync<HistoryItem>();
        }
    }
}
