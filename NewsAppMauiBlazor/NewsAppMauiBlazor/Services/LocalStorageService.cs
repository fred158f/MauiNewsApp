using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsAppMauiBlazor.Services
{
    public class LocalStorageService
    {
        private readonly ISecureStorage secureStorage;

        public LocalStorageService(ISecureStorage _secureStorage) 
        {
            secureStorage = _secureStorage;
        }

        public void ClearStorage()
        {
            secureStorage.RemoveAll();
        }

        public void RemoveObject(string key)
        {
            secureStorage.Remove(key);
        }

        public async Task<bool> ObjectExists(string name)
        {
            object obj = null;
            obj = await GetObject<object>(name);
            return obj != null;
        }

        public async void SaveObject<T>(T _object, string Name) where T : class
        {
            string jsonObject = JsonConvert.SerializeObject(_object);
            await secureStorage.SetAsync(Name, jsonObject);
        }

        public async Task<T> GetObject<T>(string Name) where T : class
        {
            string jsonObject = await secureStorage.GetAsync(Name);
            T obj = null;
            if (jsonObject != null)
            {
                obj = JsonConvert.DeserializeObject<T>(jsonObject);
            }
            return obj ?? default;
        }

        public async void RemoveFromCollection(string name, string collection)
        {
            List<string> list;

            list = await GetObject<List<string>>(collection) ?? new List<string>();

            list.Remove(name);
            RemoveObject(name);
            SaveObject(list, collection);
        }

        public async void SaveObjectToCollection<T>(T _object, string name, string collection) where T : class
        {
            List<string> list;

            list = await GetObject<List<string>>(collection) ?? new List<string>();

            list.Add(name);

            SaveObject(_object, name);
            SaveObject(list, collection);
        }

        public async Task<T[]> GetCollection<T>(string collection) where T : class
        {
            List<T> objectsToReturn = new List<T>();
            string[] storedObjects = null;
         
            storedObjects = await GetObject<string[]>(collection);
            
            if (storedObjects != null)
            {
                foreach (string objectLocation in storedObjects)
                {
                    T obj = await GetObject<T>(objectLocation);

                    if (obj != null)
                        objectsToReturn.Add(obj);
                }
            }

            return objectsToReturn?.ToArray();
        }
    }
}
