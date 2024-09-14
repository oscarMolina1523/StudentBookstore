using Google.Cloud.Firestore;

namespace IntegrationFirebaseApi.Models.Entity
{
    [FirestoreData]
    public class Material{

        public Material()
        {
            
        }

        [FirestoreProperty]
        public string Id { get; set; }

        [FirestoreProperty]
        public string Descripcion { get; set; }

        [FirestoreProperty]
        public bool Estado { get; set; }
        
    }
}