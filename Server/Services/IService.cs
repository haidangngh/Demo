namespace Server.Services
{
	
		public interface IGenericService<T> where T : class
		{
			Task Create(T entity);
			Task Delete(Guid id);
			Task<List<T>> GetAll();
			Task<T> GetById(Guid id);
			Task Update(T entity);
		}
	
}
