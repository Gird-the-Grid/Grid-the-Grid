using BlazorServerAPI.Models.Entities;
using BlazorServerAPI.Settings;
using MongoDB.Driver;
using System.Threading.Tasks;

namespace BlazorServerAPI.Repository
{
    public class SecurityRepository : BaseRepository<PenetrationReportModel>
    {
        public SecurityRepository(IMongoDbSettings settings) : base(settings, "attacks")
        { }

        public async Task<PenetrationReportModel> FindPenetrationReportByEmail(string email)
        {
            return (await _documents.FindAsync<PenetrationReportModel>(report => report.Email == email)).SingleOrDefault();
        }

        public async Task<PenetrationReportModel> RegisterNewAttack(string email, string ip)
        {
            var report = await FindPenetrationReportByEmail(email);
            if (report == null)
            {
                report = new PenetrationReportModel {Email = email};
                report.AddIp(ip);
                ++report.PasswordAtempts;
                await _documents.InsertOneAsync(report);
            }
            else
            {
                report.AddIp(ip);
                if (++report.PasswordAtempts > 10)
                {
                    await _documents.FindOneAndDeleteAsync(document => document.Id == report.Id);
                }
                else
                {
                    await Update(report.Id, report);
                }
            }
            return report;
        }
    }
}
