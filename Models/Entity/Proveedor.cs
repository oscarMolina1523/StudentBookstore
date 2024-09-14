using Google.Cloud.Firestore;

namespace IntegrationFirebaseApi.Models.Entity
{
    [FirestoreData]
    public class Proveedor{

        public Proveedor()
        {
            
        }
        [FirestoreProperty]
        public string Id { get; set; }

        [FirestoreProperty]
        public string Direccion { get; set; }

        [FirestoreProperty]
        public string Telefono { get; set; }

        [FirestoreProperty]
        public string CorreoElectronico {get; set;}

        [FirestoreProperty]
        public string Descripcion {get; set;}

    }
}
