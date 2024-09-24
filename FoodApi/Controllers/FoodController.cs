using FoodApi.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace FoodApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FoodController : ControllerBase
    {
        private readonly FoodContext _foodContext;
        public FoodController(FoodContext foodContext)
        {
            _foodContext = foodContext;
        }
        //API for get
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Food>>> GetFoods()
        {
            if (_foodContext.Foods == null)
            {
                return NotFound();
            }
            return await _foodContext.Foods.ToListAsync();
        }
        //API for get by id
        [HttpGet("{id}")]
        public async Task<ActionResult<Food>> GetFood(int id)
        {
            if (_foodContext.Foods == null)
            {
                return NotFound();
            }
            var food = await _foodContext.Foods.FindAsync(id);
            if (food == null)
            {
                return NotFound();
            }


            return food;
        }

        [HttpPost]
        public async Task<ActionResult<Food>> PostFood(Food food)
        {
            _foodContext.Foods.Add(food);
            await _foodContext.SaveChangesAsync();
            return CreatedAtAction(nameof(GetFood), new { id = food.ID }, food);


        }
        [HttpPut("{id}")]
        public async Task<ActionResult> PutFood(int id, Food food)
        {
            if (id != food.ID)
            {
                return BadRequest();
            }
            _foodContext.Entry(food).State = EntityState.Modified;
            try
            {
                await _foodContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;


            }
            return Ok();

        }

        [HttpDelete]
        public async Task<ActionResult> DeleteFood(int id)
        {
            if (_foodContext.Foods == null)
            {
                return NotFound();



            }
            var food = await _foodContext.Foods.FindAsync(id);
            if (food == null)
            {
                return NotFound();
            }
            _foodContext.Foods.Remove(food);
            await _foodContext.SaveChangesAsync();
            return Ok();
        }

            

    }
}
    

    

