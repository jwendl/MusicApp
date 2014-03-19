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
    public class SongsController
        : EntitySetController<Song, int>
    {
        private IRepository<Song> songRepository { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="MuscleGroupsController" /> class.
        /// </summary>
        /// <param name="songRepository">The workout day business model.</param>
        public SongsController(IRepository<Song> songRepository)
        {
            this.songRepository = songRepository;
        }

        /// <summary>
        /// GET /MuscleGroups
        /// GET /MuscleGroups?$filter=startswith(Name,'Grid')
        /// </summary>
        /// <returns></returns>
        public override IQueryable<Song> Get()
        {
            return songRepository.AsQueryable();
        }

        /// <summary>
        /// Gets the key.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        protected override int GetKey(Song entity)
        {
            return entity.Id;
        }

        /// <summary>
        /// Gets the entity by key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        protected override Song GetEntityByKey(int key)
        {
            return songRepository.Get(key);
        }

        /// <summary>
        /// Creates the entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        protected override Song CreateEntity(Song entity)
        {
            songRepository.Add(entity);
            return entity;
        }

        /// <summary>
        /// Updates the entity.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="update">The update.</param>
        /// <returns></returns>
        protected override Song UpdateEntity(int key, Song update)
        {
            songRepository.Update(update);
            return update;
        }

        /// <summary>
        /// Deletes the specified key.
        /// </summary>
        /// <param name="key">The key.</param>
        public override void Delete([FromODataUri]int key)
        {
            songRepository.Delete(key);
        }
    }
}
