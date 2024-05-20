﻿using FlavorCart.Interfaces;
using Google.Cloud.Firestore;
using System.Security.Policy;

namespace FlavorCart.Models
{
    [FirestoreData]
    public class Recipe : IList , IBaseFirestoreData
    {
        
        public string Id { get; set; }
        [FirestoreProperty]
        public List<ListItem> ArticleList { get ; set ; }
        [FirestoreProperty]
        public string Name { get ; set ; }
        [FirestoreProperty]
        public string UserId { get ; set ; }
    
        [FirestoreProperty]
        public float TotalPrice { get; set ; }

        [FirestoreProperty]
        public bool IsRecipe { get ; set ; } = true;

        [FirestoreProperty]
        public string Description { get; set; } 
    }
}
