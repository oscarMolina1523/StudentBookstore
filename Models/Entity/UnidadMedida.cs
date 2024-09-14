using Google.Cloud.Firestore;

namespace IntegrationFirebaseApi.Models.Entity
{
    [FirestoreData]
    public class UnidadMedida{
        public UnidadMedida()
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
