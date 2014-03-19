using MusicApp.Data;
using SharpRepository.Repository;
using System.Linq;
using System.Web.Http.OData;

namespace MusicApp.Controllers.WebApi
{
    /// <summary>
    /// 
    /// </summary>
    [ODataRouting]
    [ODataFormatting]
    public class AlbumsController
        : EntitySetController<Album, int>
    {
        private IRepository<Album> albumRepository { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="MuscleGroupsController" /> class.
        /// </summary>
        /// <param name="albumRepository">The workout day business model.</param>
        public AlbumsController(IRepository<Album> albumRepository)
        {
            this.albumRepository = albumRepository;
        }

        /// <summary>
        /// GET /MuscleGroups
        /// GET /MuscleGroups?$filter=startswith(Name,'Grid')
        /// </summary>
        /// <returns></returns>
        public override IQueryable<Album> Get()
        {
            return albumRepository.AsQueryable();
        }

        /// <summary>
        /// Gets the key.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        protected override int GetKey(Album entity)
        {
            return entity.Id;
        }

        /// <summary>
        /// Gets the entity by key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        protected override Album GetEntityByKey(int key)
        {
            return albumRepository.Get(key);
        }

        /// <summary>
        /// Creates the entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        protected override Album CreateEntity(Album entity)
        {
            albumRepository.Add(entity);
            return entity;
        }

        /// <summary>
        /// Updates the entity.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="update">The update.</param>
        /// <returns></returns>
        protected override Album UpdateEntity(int key, Album update)
        {
            albumRepository.Update(update);
            return update;
        }

        /// <summary>
        /// Deletes the specified key.
        /// </summary>
        /// <param name="key">The key.</param>
        public override void Delete([FromODataUri]int key)
        {
            albumRepository.Delete(key);
        }
    }
}
