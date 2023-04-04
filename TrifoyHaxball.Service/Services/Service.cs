using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TrifoyHaxball.Core.DTOs;
using TrifoyHaxball.Core.Repositories;
using TrifoyHaxball.Core.Services;
using TrifoyHaxball.Core.UnitOfWorks;
using TrifoyHaxball.Entity;
using TrifoyHaxball.Service.Exceptions;

namespace TrifoyHaxball.Service.Services
{
    public class Service<T,T2> : IService<T,T2> where T : class where T2 : class
    {
        private readonly IGenericRepository<T> _repository;
        private readonly IUnitOfWork _unitOfWork;
        protected readonly IMapper _mapper;

        public Service(IGenericRepository<T> repository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<T> AddAsync(T entity)
        {
            await _repository.AddAsync(entity);
            await _unitOfWork.CommitAsync();
            return _mapper.Map<T>(entity);
        }

        public async Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> entities)
        {
            await _repository.AddRangeAsync(entities);
            await _unitOfWork.CommitAsync();
            return entities;
        }

        public async Task<bool> AnyAsync(Expression<Func<T, bool>> expression)
        {
            return await _repository.AnyAsync(expression);
        }

        public async Task<IEnumerable<T2>> GetAllAsync()
        {
            var players = await _repository.GetAll().ToListAsync();
            
            return _mapper.Map<IEnumerable<T2>>(players);
        }

        public async Task<T2> GetByIdAsync(int id)
        {
            var player= await _repository.GetByIdAsync(id);
            if (player is null)
            {
                throw new NotFoundException($"{typeof(T).Name}({id}) bulunamadı!");
            }
            return _mapper.Map<T2>(player);
        }

        public async Task RemoveAsync(int id)
        {
            var player = await _repository.GetByIdAsync(id);
            if (player is null)
            {
                throw new NotFoundException($"{typeof(T).Name}({id}) bulunamadı!");
            }
            _repository.Remove(player);
            await _unitOfWork.CommitAsync();
        }

        public async Task RemoveRangeAsync(IEnumerable<T> entities)
        {
            _repository.RemoveRange(entities);
            await _unitOfWork.CommitAsync();
        }

        public async Task UpdateAsync(T entity)
        {
            _repository.Update(entity);
            await _unitOfWork.CommitAsync();
        }

        public IQueryable<T> Where(Expression<Func<T, bool>> expression)
        {
            return _repository.Where(expression);
        }
    }
}
