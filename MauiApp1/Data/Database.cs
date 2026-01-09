using MauiApp1.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MauiApp1.Data
{
    public class Database
    {
        private readonly SQLiteAsyncConnection _database; // тут ми создаєм ДБ 1 раз і всьо(readonly)

        public Database(string dbPath) // це ми або создаєм,або,якщо є,то просто откриваєм БД
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<Groups>().Wait();
            _database.CreateTableAsync<Items>().Wait();

        }
        public Task<List<Groups>> GetGroupsAsync()
        {
            return _database.Table<Groups>().ToListAsync();
        }
        public Task<List<Items>> GetItemsByGroupAsync(string groupId)
        {
            return _database.Table<Items>()
                            .Where(i => i.type == groupId)
                            .ToListAsync();
        }

        public Task<int> SaveGroupAsync(Groups group)
        {
            if (group.id != 0)
                return _database.UpdateAsync(group);
            else
                return _database.InsertAsync(group);
        }
        public Task<int> SaveItemAsync(Items item) // це ми создаєм або оновлюєм наш Item
                                                   // (хз,як оце все назвать,ячейка данних)
        {
            if (item.id != 0)
                return _database.UpdateAsync(item);
            else
                return _database.InsertAsync(item);
        }

        public Task<int> DeleteItemAsync(Items item) //ну а тут удаляєм
        {
            return _database.DeleteAsync(item);
        }
    }
}
