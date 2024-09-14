using Google.Cloud.Firestore;

namespace IntegrationFirebaseApi.Models.Entity
{
    [FirestoreData]
    public class Empleado{

        public Empleado()
        {
            
        }
        [FirestoreProperty]
        public string Id { get; set; }

        [FirestoreProperty]
        public string Nombres { get; set; }

        [FirestoreProperty]
        public string Apellidos { get; set; }

        [FirestoreProperty]
        public string Cedula {get; set;}

        [FirestoreProperty]
        public string Telefono {get; set;}

        [FirestoreProperty]
        public bool Estado {get; set;}
        
    }
}
