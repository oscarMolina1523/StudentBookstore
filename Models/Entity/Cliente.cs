using Google.Cloud.Firestore;

namespace IntegrationFirebaseApi.Models.Entity
{
    [FirestoreData]
    public class Cliente{
        public Cliente()
        {
        
        }

        [FirestoreProperty]
        public string Id { get; set; }

        [FirestoreProperty]
        public string Nombres { get; set; }

        [FirestoreProperty]
        public string Cedula { get; set; }

        [FirestoreProperty]
        public string Telefono {get; set;}
        
    }
}
