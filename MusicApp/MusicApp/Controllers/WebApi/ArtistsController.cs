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
    public class ArtistsController
        : EntitySetController<Artist, int>
    {
        private IRepository<Artist> artistRepository { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="MuscleGroupsController" /> class.
        /// </summary>
        /// <param name="artistRepository">The workout day business model.</param>
        public ArtistsController(IRepository<Artist> artistRepository)
        {
            this.artistRepository = artistRepository;
        }

        /// <summary>
        /// GET /MuscleGroups
        /// GET /MuscleGroups?$filter=startswith(Name,'Grid')
        /// </summary>
        /// <returns></returns>
        public override IQueryable<Artist> Get()
        {
            return artistRepository.AsQueryable();
        }

        /// <summary>
        /// Gets the key.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        protected override int GetKey(Artist entity)
        {
            return entity.Id;
        }

        /// <summary>
        /// Gets the entity by key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        protected override Artist GetEntityByKey(int key)
        {
            return artistRepository.Get(key);
        }

        /// <summary>
        /// Creates the entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        protected override Artist CreateEntity(Artist entity)
        {
            artistRepository.Add(entity);
            return entity;
        }

        /// <summary>
        /// Updates the entity.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="update">The update.</param>
        /// <returns></returns>
        protected override Artist UpdateEntity(int key, Artist update)
        {
            artistRepository.Update(update);
            return update;
        }

        /// <summary>
        /// Deletes the specified key.
        /// </summary>
        /// <param name="key">The key.</param>
        public override void Delete([FromODataUri]int key)
        {
            artistRepository.Delete(key);
        }
    }
}
