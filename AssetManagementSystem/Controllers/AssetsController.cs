using AssetManagementSystem.Models;
using AssetManagementSystem.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AssetManagementSystem.Controllers
{
    [Route("api/[controller]")] // attribute-based routing
    [ApiController] // since it is an API application
    public class AssetsController : ControllerBase
    {
        // Call repository
        private readonly IAssetRepository _repository;

        // DI Constructor Injection
        public AssetsController(IAssetRepository repository)
        {
            _repository = repository;
        }

        // GET: api/<AssetsController>

        #region 1- Get all assets - search all
        [HttpGet] // HTTP attribute
      
        public async Task<ActionResult<IEnumerable<Asset>>> GetAllAssets() // first endpoint created
        {
            var assets = await _repository.GetAssets();
            if (assets == null)
            {
                return NotFound("No Assets found");
            }

            return Ok(assets.Value);
        }
        #endregion

        #region 2- Get asset by ID
        [HttpGet("{id}")]
        public async Task<ActionResult<Asset>> GetAssetById(int id)
        {
            var asset = await _repository.GetAssetById(id);
            if (asset == null)
            {
                return NotFound("No Asset found");
            }

            return Ok(asset.Value);
        }
        #endregion

        #region 3- Insert an asset - Return Asset Record
        [HttpPost]
        public async Task<ActionResult<Asset>> InsertAssetReturnRecord(Asset asset)
        {
            if (ModelState.IsValid)
            {
                var newAsset = await _repository.AddAsset(asset);
                if (newAsset != null)
                {
                    return Ok(newAsset.Value);
                }
                else
                {
                    return NotFound();
                }
            }
            return BadRequest();
        }
        #endregion

        #region 4- Insert an asset - Return Asset ID
        [HttpPost("v1")]
        public async Task<ActionResult<int>> InsertAssetReturnId(Asset asset)
        {
            if (ModelState.IsValid)
            {
                var newAssetId = await _repository.AddAssetReturnId(asset);
                if (newAssetId != null)
                {
                    return Ok(newAssetId.Value);
                }
                else
                {
                    return NotFound();
                }
            }
            return BadRequest();
        }
        #endregion

        #region 5- Update asset - Return Asset Record
        [HttpPut("{id}")] // whatever inside curly braces is the value
        public async Task<ActionResult<Asset>> UpdateAsset(int id, Asset asset)
        {
            if (ModelState.IsValid)
            {
                var updatedAsset = await _repository.UpdateAsset(id, asset);
                if (updatedAsset != null)
                {
                    return Ok(updatedAsset.Value);
                }
                else
                {
                    return NotFound();
                }
            }
            return BadRequest();
        }
        #endregion

        #region 6- Delete asset
        [HttpDelete("{id}")]
        public IActionResult DeleteAsset(int id)
        {
            try
            {
                var result = _repository.DeleteAsset(id);
                if (result.Value == null)
                {
                    return NotFound(new
                    {
                        success = false,
                        message = "Asset could not be deleted or not found"
                    });
                }
                return Ok(result.Value);
            }
            catch (Exception ex)
            {
                // Log exception in real-world scenarios
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new { success = false, message = "An unexpected error occurred" });
            }
        }
        #endregion

        #region 7- Insert an asset using Stored Procedure
        [HttpPost("p1")]
        public async Task<ActionResult<IEnumerable<Asset>>> InsertAssetByProcedure(Asset asset)
        {
            if (ModelState.IsValid)
            {
                var newAsset = await _repository.AddAssetByProcedure(asset);
                if (newAsset != null)
                {
                    return Ok(newAsset.Value);
                }
                else
                {
                    return NotFound();
                }
            }
            return BadRequest();
        }
        #endregion
    }
}
