using api.Data;
using api.Dtos.Stock;
using api.Mappers;
using Microsoft.AspNetCore.Mvc;


namespace api.Controllers
{
    [Route("/api/stock")]
    [ApiController]
    public class StockController : ControllerBase
    {
        // private readonly ILogger<StockController> _logger;

        private readonly ApplicationDBContext _context;
        public StockController(ApplicationDBContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetAll(){
            var stocks = _context.Stocks.ToList()
            .Select(s => s.ToStockDto());

            return Ok(stocks);
        }

        [HttpGet("{id}")]
        public IActionResult GetById([FromRoute] int id){
            var stock = _context.Stocks.Find(id);

            if(stock == null){
                return NotFound();
            }

            return Ok(stock.ToStockDto());
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateStockRequestDto stockDto)
        {

            var stockModel = stockDto.ToStockFromCreateDTO();

            _context.Stocks.Add(stockModel);

            _context.SaveChanges();

            // await _stockRepo.CreateAsync(stockModel);

            return CreatedAtAction(nameof(GetById), new { id = stockModel.Id }, stockModel.ToStockDto());
        }

        [HttpPut("{id}")]
        public IActionResult Update([FromRoute] int id, [FromBody] UpdateStockRequestDto updateDto){

            var stockModel = _context.Stocks.FirstOrDefault(x => x.Id == id);

            if(stockModel == null){
                return NotFound();
            }

            stockModel.CompanyName = updateDto.CompanyName;
            stockModel.Industry = updateDto.Industry;
            stockModel.Symbol = updateDto.Symbol;
            stockModel.Purchase = updateDto.Purchase;
            stockModel.MarketCap = updateDto.MarketCap;
            stockModel.LastDiv = updateDto.LastDiv;

            _context.SaveChanges();

            return Ok(stockModel.ToStockDto());
        }

        
    }
}