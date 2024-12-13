using AssetManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AssetManagementSystem.Repository
{
    public class AssetRepository : IAssetRepository
    {
        private readonly AssetManagementDbContext _context;

        public AssetRepository(AssetManagementDbContext context)
        {
            _context = context;
        }

        #region 1 - Get All Assets - Search All
        public async Task<ActionResult<IEnumerable<Asset>>> GetAssets()
        {
            try
            {
                if (_context != null)
                {
                    return await _context.Assets.ToListAsync();
                }
                return new List<Asset>();
            }
            catch (Exception ex)
            {
                // Log exception here if needed
                return null;
            }
        }
        #endregion

       

        #region 3 - Search By Id
        public async Task<ActionResult<Asset>> GetAssetById(int id)
        {
            try
            {
                if (_context != null)
                {
                    var asset = await _context.Assets.FirstOrDefaultAsync(a => a.AmId == id);
                    return asset;
                }
                return null;
            }
            catch (Exception ex)
            {
                // Log exception here if needed
                return null;
            }
        }
        #endregion

        #region 4 - Insert an Asset - Return Asset Record
        public async Task<ActionResult<Asset>> AddAsset(Asset asset)
        {
            try
            {
                if (asset == null)
                {
                    throw new ArgumentNullException(nameof(asset), "Asset data is null");
                }
                if (_context == null)
                {
                    throw new InvalidOperationException("Database context is not initialized");
                }

                await _context.Assets.AddAsync(asset);
                await _context.SaveChangesAsync();

                var addedAsset = await _context.Assets.FirstOrDefaultAsync(a => a.AmId == asset.AmId);
                return addedAsset;
            }
            catch (Exception ex)
            {
                // Log exception here if needed
                return null;
            }
        }
        #endregion

        #region 5 - Insert an Asset - Return ID
        public async Task<ActionResult<int>> AddAssetReturnId(Asset asset)
        {
            try
            {
                if (asset == null)
                {
                    throw new ArgumentNullException(nameof(asset), "Asset data is null");
                }
                if (_context == null)
                {
                    throw new InvalidOperationException("Database context is not initialized");
                }

                await _context.Assets.AddAsync(asset);
                var changes = await _context.SaveChangesAsync();

                if (changes > 0)
                {
                    return asset.AmId;
                }
                else
                {
                    throw new Exception("Failed to save asset to the database");
                }
            }
            catch (Exception ex)
            {
                // Log exception here if needed
                return null;
            }
        }
        #endregion

        #region 6 - Update an Asset with ID and Asset
        public async Task<ActionResult<Asset>> UpdateAsset(int id, Asset asset)
        {
            try
            {
                if (asset == null)
                {
                    throw new ArgumentNullException(nameof(asset), "Asset data is null");
                }
                if (_context == null)
                {
                    throw new InvalidOperationException("Database context is not initialized");
                }

                var existingAsset = await _context.Assets.FindAsync(id);
                if (existingAsset == null)
                {
                    return null;
                }

                existingAsset.AmModel = asset.AmModel;
                existingAsset.AmMyyear = asset.AmMyyear;
                existingAsset.AmPdate = asset.AmPdate;
                existingAsset.AmSnumber = asset.AmSnumber;
                existingAsset.AmStatus = asset.AmStatus;
                existingAsset.AmWarranty = asset.AmWarranty;

                await _context.SaveChangesAsync();

                var updatedAsset = await _context.Assets.FirstOrDefaultAsync(a => a.AmId == asset.AmId);
                return updatedAsset;
            }
            catch (Exception ex)
            {
                // Log exception here if needed
                return null;
            }
        }
        #endregion

        #region 7 - Delete an Asset
        public JsonResult DeleteAsset(int id)
        {
            try
            {
                if (id == 0)
                {
                    return new JsonResult(new
                    {
                        success = false,
                        message = "Invalid asset ID"
                    })
                    {
                        StatusCode = StatusCodes.Status400BadRequest
                    };
                }

                if (_context == null)
                {
                    return new JsonResult(new
                    {
                        success = false,
                        message = "Database context is not initialized."
                    })
                    {
                        StatusCode = StatusCodes.Status500InternalServerError
                    };
                }

                var existingAsset = _context.Assets.Find(id);
                if (existingAsset == null)
                {
                    return new JsonResult(new
                    {
                        success = false,
                        message = "Asset not found."
                    })
                    {
                        StatusCode = StatusCodes.Status404NotFound
                    };
                }

                _context.Assets.Remove(existingAsset);
                _context.SaveChangesAsync();

                return new JsonResult(new
                {
                    success = true,
                    message = "Asset deleted successfully."
                })
                {
                    StatusCode = StatusCodes.Status200OK
                };
            }
            catch (Exception ex)
            {
                return new JsonResult(new
                {
                    success = false,
                    message = "An error occurred while deleting the asset."
                })
                {
                    StatusCode = StatusCodes.Status500InternalServerError
                };
            }
        }
        #endregion


        #region 9 - Insert an Asset using Stored Procedure
        public async Task<ActionResult<IEnumerable<Asset>>> AddAssetByProcedure(Asset asset)
        {
            try
            {
                if (asset == null)
                {
                    throw new ArgumentNullException(nameof(asset), "Asset data is null");
                }
                if (_context == null)
                {
                    throw new InvalidOperationException("Database context is not initialized");
                }

                var result = await _context.Assets.FromSqlRaw(
                    "EXEC InsertAsset @AmModel, @AmMyyear, @AmPdate, @AmSnumber, @AmStatus, @AmWarranty",
                    new SqlParameter("@AmModel", asset.AmModel),
                    new SqlParameter("@AmMyyear", asset.AmMyyear),
                    new SqlParameter("@AmPdate", asset.AmPdate),
                    new SqlParameter("@AmSnumber", asset.AmSnumber),
                    new SqlParameter("@AmStatus", asset.AmStatus),
                    new SqlParameter("@AmWarranty", asset.AmWarranty)
                ).ToListAsync();

                if (result != null && result.Count > 0)
                {
                    return result;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                // Log exception here if needed
                return null;
            }
        }
        #endregion
    }
}
