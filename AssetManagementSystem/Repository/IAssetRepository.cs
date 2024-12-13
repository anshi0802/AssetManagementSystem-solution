using AssetManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AssetManagementSystem.Repository
{
    public interface IAssetRepository
    {
        // 1- Get all assets
        Task<ActionResult<IEnumerable<Asset>>> GetAssets();

        // 2 - Get All Assets with Details (ViewModel equivalent)
        

        // 3- Search by Id
        Task<ActionResult<Asset>> GetAssetById(int id);

        // 4- Insert an asset and return the asset record
        Task<ActionResult<Asset>> AddAsset(Asset asset);

        // 5- Insert an asset and return the asset ID
        Task<ActionResult<int>> AddAssetReturnId(Asset asset);

        // 6- Update an asset with ID and asset
        Task<ActionResult<Asset>> UpdateAsset(int id, Asset asset);

        // 7- Delete an asset
        JsonResult DeleteAsset(int id);

        // 8- Get All Departments (equivalent to GetTblDepartments)
      

        // 9- Insert an asset using stored procedure
        Task<ActionResult<IEnumerable<Asset>>> AddAssetByProcedure(Asset asset);
    }
}
