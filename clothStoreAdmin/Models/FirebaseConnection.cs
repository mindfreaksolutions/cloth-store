using Firebase.Auth;
using Firebase.Database;
using Firebase.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace clothStoreAdmin.Models
{
    public class FirebaseConnection
    {
        public static FirebaseClient FirebaseDatabase()
        {
            FirebaseClient firebase = new FirebaseClient("https://cloth-store.firebaseio.com/");
            return firebase;
        }

        public static FirebaseAuthProvider FirebaseAuthentication()
        {
            FirebaseAuthProvider fa = new FirebaseAuthProvider(new FirebaseConfig("AIzaSyC6BDbbR38nKmOp9M-btfQPboI-d0uavvM"));
            return fa;
        }

        public static FirebaseStorage FirebaseStorageConnection()
        {
            FirebaseStorage storageConn = new FirebaseStorage("cloth-store.appspot.com");
            return storageConn;
        }
    }
}