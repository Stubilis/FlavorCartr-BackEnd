﻿using FlavorCart.Models;
using FlavorCart.Enums;

using Google.Cloud.Firestore;

namespace FlavorCart.Repositories
{
    public class ArticleRepository
    {
        private readonly BaseRepository<Article> _repository;
        public ArticleRepository()
        {
            _repository = new BaseRepository<Article>(Collection.Articles);
        }

        public async Task<List<Article>> GetAllAsync() => await _repository.GetAllAsync<Article>();

        public async Task<Article?> GetAsync(Article entity) => (Article?)await _repository.GetAsync(entity);

        public async Task<Article> AddAsync(Article entity) => await _repository.AddAsync(entity);

        public async Task<Article> UpdateAsync(Article entity) => await _repository.UpdateAsync(entity);

        public async Task DeleteAsync(Article entity) => await _repository.DeleteAsync(entity);

        public async Task<List<Article>> QueryRecordsAsync(Query query) => await _repository.QueryRecordsAsync<Article>(query);

        // This is specific to Articles.

        public async Task<List<Article>> GetArticleByCategory(string catId)
        {
            var query = _repository._firestoreDb.Collection("Articles").WhereArrayContains("Categories",catId);
            return await this.QueryRecordsAsync(query);
        }



    }
}
