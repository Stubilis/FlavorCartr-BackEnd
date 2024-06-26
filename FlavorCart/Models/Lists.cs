﻿using FlavorCart.Interfaces;
using Google.Cloud.Firestore;
using System.Globalization;

namespace FlavorCart.Models
{
    [FirestoreData]
    public class Lists : IList, IBaseFirestoreData
    {

        public string Id { get; set; }

        [FirestoreProperty]
        public string Name { get; set; }

        [FirestoreProperty]
        public List<ListItem> ArticleList { get; set; } //Save here the articles id + amount

        [FirestoreProperty]
        public float TotalPrice { get; set; } = 0;

        [FirestoreProperty]
        public string UserId { get; set; }

        [FirestoreProperty]
        public string CreationDate { get; set; }

        [FirestoreProperty]
        public bool IsPublic { get; set; }
        public void setCreationDate()
        {
          
            this.CreationDate = DateTime.Now.ToString();

        }
     
        
    }

}
