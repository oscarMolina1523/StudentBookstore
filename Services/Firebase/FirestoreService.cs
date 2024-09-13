using Google.Cloud.Firestore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IntegrationFirebaseApi.Services
{
    public class FirestoreService<T> where T : class
    {
        private readonly FirestoreDb _firestoreDb;
        private readonly string _collectionName;

        public FirestoreService(string collectionName)
        {
            _firestoreDb = FirestoreDb.Create("libreriaestudiante-76afb");
            _collectionName = collectionName;
        }

        public async Task<T> CreateDocument(T entity)
        {
            try
            {
                DocumentReference docRef = _firestoreDb.Collection(_collectionName).Document();
                var propertyInfo = entity.GetType().GetProperty("Id");
                if (propertyInfo != null)
                {
                    propertyInfo.SetValue(entity, docRef.Id);
                }
                await docRef.SetAsync(entity);
                return entity;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }

        public async Task<List<T>> GetAllDocuments()
        {
            try
            {
                QuerySnapshot snapshot = await _firestoreDb.Collection(_collectionName).GetSnapshotAsync();
                List<T> documents = new List<T>();

                foreach (DocumentSnapshot document in snapshot.Documents)
                {
                    if (document.Exists)
                    {
                        T entity = document.ConvertTo<T>();
                        documents.Add(entity);
                    }
                }

                return documents;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }

        public async Task<T> GetDocumentById(string documentId)
        {
            try
            {
                DocumentReference docRef = _firestoreDb.Collection(_collectionName).Document(documentId);
                DocumentSnapshot snapshot = await docRef.GetSnapshotAsync();

                if (snapshot.Exists)
                {
                    return snapshot.ConvertTo<T>();
                }

                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }

        public async Task<T> UpdateDocument(string documentId, Dictionary<string, object> updates)
        {
            try
            {
                //este DocumentReference solo es una referencia del documento no el documento
                DocumentReference docRef = _firestoreDb.Collection(_collectionName).Document(documentId);
                await docRef.UpdateAsync(updates);

                DocumentSnapshot updatedSnapshot = await docRef.GetSnapshotAsync();// Obtengo los datos ya actualizado
                return updatedSnapshot.ConvertTo<T>();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }

        public async Task<bool> DeleteDocument(string documentId)
        {
            try
            {
                DocumentReference docRef = _firestoreDb.Collection(_collectionName).Document(documentId);
                await docRef.DeleteAsync();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return false;
            }
        }
    }
}
