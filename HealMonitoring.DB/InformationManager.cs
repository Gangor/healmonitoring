using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealMonitoring.DB
{
    public class InformationManager
    {
        private HealMonitoringContext _context;

        public InformationManager(
            HealMonitoringContext context)
        {
            _context = context;
        }

        public async Task<bool> Create(Information information)
        {
            if (information == null)
                throw new ArgumentNullException(nameof(information));

            _context.Add(information);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return false;
            }

            return true;
        }

        public async Task<bool> CreateRange(Information[] informations)
        {
            if (informations == null)
                throw new ArgumentNullException(nameof(informations));

            _context.AddRange(informations);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                Console.WriteLine("test");
                return false;
            }

            return true;
        }

        public async Task<List<Information>> FindByInformationByUser(int userId)
        {
            return _context.Information.Where(u => u.ID_User == userId)
                                       .ToList();
        }

        public async Task<bool> Update(Information information)
        {
            if (information == null)
                throw new ArgumentNullException(nameof(information));

            _context.Attach(information);
            _context.Update(information);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return false;
            }

            return true;
        }

        public async Task<bool> Replace(int UserId, string dataSource, List<Information> information)
        {
            if (information == null)
                throw new ArgumentNullException(nameof(information));

            
            try
            {
                await DeleteRange(UserId, dataSource);
                await CreateRange(information.ToArray());
            }
            catch (DbUpdateConcurrencyException)
            {
                return false;
            }

            return true;
        }

        public async Task<bool> Delete(Information information)
        {
            if (information == null)
                throw new ArgumentNullException(nameof(information));

            _context.Remove(information);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return false;
            }
            return true;
        }

        public async Task<bool> DeleteRange(int userId, string dataSource)
        {
            var data = await _context.Information.Where(u => u.ID_User == userId && u.DataSources == dataSource).ToListAsync();

            try
            {
                await DeleteRange(data.ToArray());
            }
            catch (DbUpdateConcurrencyException)
            {
                return false;
            }

            return true;
        }

        public async Task<bool> DeleteRange(Information[] informations)
        {
            if (informations == null)
                throw new ArgumentNullException(nameof(informations));

            _context.RemoveRange(informations);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return false;
            }
            return true;
        }
    }
}
