using Google.Cloud.Firestore;
using IntegrationFirebaseApi.Models.Entity;
using IntegrationFirebaseApi.Models.Dtos;

namespace IntegrationFirebaseApi.Services
{
    public class FirestoreService
    {
        private readonly FirestoreDb _firestoreDb;
        private const string collectionName = "User";

        public FirestoreService(){
            _firestoreDb = FirestoreDb.Create("libreriaestudiante-76afb");
        }

        public async Task<UserEntity> CreateUser(UserEntity user){
            try{
                DocumentReference doc = _firestoreDb.Collection(collectionName).Document();
                user.Id= doc.Id;
                await doc.SetAsync(user);
                return user;
            }catch(Exception ex){
                Console.WriteLine(ex);
                throw;
            }
        }

        public async Task<List<UserEntity>> GetAllUsers(){
            try{
                //recorda QuerySnapshot son uno o varios documentos que se pueden almacenar
                //y DocumentSnapshot es uno unicamente
                QuerySnapshot snapshot = await _firestoreDb.Collection(collectionName).GetSnapshotAsync();
                List<UserEntity> users = new List<UserEntity>();
                
                foreach (DocumentSnapshot document in snapshot.Documents)
                {
                    if (document.Exists)
                    {
                        UserEntity user = document.ConvertTo<UserEntity>(); 
                        users.Add(user);
                    }
                }
                return users;
            }catch(Exception ex){
                Console.WriteLine(ex);
                throw;
            }
        }

        public async Task<UserEntity> GetUserById(string id)
        {
            try
            {
                DocumentReference docRef = _firestoreDb.Collection(collectionName).Document(id);
                DocumentSnapshot snapshot = await docRef.GetSnapshotAsync();

                if (snapshot.Exists)
                {
                    return snapshot.ConvertTo<UserEntity>(); // recuerda mae siempre hay que convertir 
                }
                else
                {
                    return null; 
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }
        
        public async Task<UserEntity> UpdateUser(string userId, UserDto userDto)
        {
            try
            {
                //este DocumentReference solo es una referencia del documento no el documento
                DocumentReference docRef = _firestoreDb.Collection(collectionName).Document(userId);
                var updateFields = new Dictionary<string, object>{
                    { "FullName", userDto.FullName },
                    { "Email", userDto.Email }
                };
                await docRef.UpdateAsync(updateFields); // Aca solo los actualiza los campos que le pasé
                DocumentSnapshot updatedSnapshot = await docRef.GetSnapshotAsync(); // Obtengo los datos ya actualizado
                return updatedSnapshot.ConvertTo<UserEntity>(); // DRetorno los datos
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }


        public async Task<bool> DeleteUser(string id)
        {
            try
            {
                DocumentReference docRef = _firestoreDb.Collection(collectionName).Document(id);
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