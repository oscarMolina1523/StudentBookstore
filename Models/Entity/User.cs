using Google.Cloud.Firestore;

namespace IntegrationFirebaseApi.Models.Entity
{
    [FirestoreData]
    public class UserEntity{

        public UserEntity(){ 

        }
        
        [FirestoreProperty]
        public string Id { get; set; }

        [FirestoreProperty]
        public string FullName { get; set; }

        [FirestoreProperty]
        public string Email { get; set;}


    }
}
