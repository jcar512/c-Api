using WebAPI.Data;

namespace WebAPI.Controllers
	{
	public class EspectaculoController
		{
		private readonly AppDbContext _context;

		public EspectaculoController(AppDbContext context)
			{
			_context = context;
			}
		}
	}
