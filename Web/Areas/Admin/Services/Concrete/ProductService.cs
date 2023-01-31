using Core.Entities;
using Core.Utilities.FileService;
using DataAccess.Repositories.Abstract;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Web.Areas.Admin.Services.Abstract;
using Web.Areas.Admin.ViewModels.Product;

namespace Web.Areas.Admin.Services.Concrete
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IProductPhotoRepository _productPhotoRepository;
        private readonly IFileService _fileService;
        private readonly ITagRepository _tagRepository;
        private readonly IProductTagRepository _productTagRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly ModelStateDictionary _modelState;
        public ProductService(IProductRepository  productRepository, IProductPhotoRepository  productPhotoRepository,
                                IActionContextAccessor actionContextAccessor,
                                IFileService fileService,
                                ITagRepository tagRepository,
                                IProductTagRepository productTagRepository,
                                IWebHostEnvironment webHostEnvironment)
        {
            _productRepository = productRepository;
            _productPhotoRepository = productPhotoRepository;
            _fileService = fileService;
            _tagRepository = tagRepository;
            _productTagRepository = productTagRepository;
            _webHostEnvironment = webHostEnvironment;
            _modelState = actionContextAccessor.ActionContext.ModelState;
        }

        public async Task<bool> CreateAsync(ProductCreateVM model)
        {
            if (!_modelState.IsValid) return false;


            bool hasError = false;
            foreach (var photo in model.ProductPhotos)
            {
                if (!_fileService.IsImage(photo))
                {
                    _modelState.AddModelError("ProductPhotos", $"{photo.FileName} yuklediyiniz file sekil formatinda olmalidir");
                    hasError = true;

                }
                else if (!_fileService.CheckSize(photo, 6000))
                {
                    _modelState.AddModelError("ProductPhotos", $"{photo.FileName} ci yuklediyiniz sekil 6000 kb dan az olmalidir");
                    hasError = true;

                }
            }

            if (hasError) { return false; }

            


            var product = new Product
            {
                Specificity = model.Specificity,
                Name = model.Name,
                Cost = model.Cost,
                OldCost = model.OldCost,
                ProductStatus = model.ProductStatus,
                Description = model.Description,
                Color = model.Color,
                MainPhotoName = await _fileService.UploadAsync(model.MainPhoto),
                CreatedAt = DateTime.Now,
            };

            await _productRepository.CreateAsync(product);

            int order = 1;
            foreach (var photo in model.ProductPhotos)
            {
                var productPhoto = new ProductPhoto
                {
                    PhotoName = await _fileService.UploadAsync(photo),
                    ProductId = product.Id,
                    Order = order

                };
                await _productPhotoRepository.CreateAsync(productPhoto);
                order++;
            }

            
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var product = await _productRepository.GetAsync(id);
            var productPhotos = await _productPhotoRepository.GetAllAsync();

            if (product != null)
            {
                foreach (var photo in productPhotos)
                {
                    _fileService.Delete(photo.PhotoName);
                    _fileService.Delete(product.MainPhotoName);
                    await _productPhotoRepository.DeleteAsync(photo);
                }
                await _productRepository.DeleteAsync(product);
                return true;
            }

            return false;
        }

        public async Task<bool> DeletePhotoAsync(int id)
        {
            var aboutUsPhoto = await _productPhotoRepository.GetAsync(id);
            if (aboutUsPhoto != null)
            {
                _fileService.Delete(aboutUsPhoto.PhotoName);

                await _productPhotoRepository.DeleteAsync(aboutUsPhoto);

                return true;

            }

            return false;
        }

        public async Task<ProductIndexVM> GetAllAsync()
        {
            var model = new ProductIndexVM
            {
                Products = await _productRepository.GetAllAsync()
            };
            return model;
        }

        public async Task<ProductUpdateVM> GetUpdateModelAsync(int id)
        {
            var product = await _productRepository.GetAsync(id);

            if (product == null) return null;

            var model = new ProductUpdateVM
            {
                Id = product.Id,
                Specificity = product.Specificity,
                Description = product.Description,
                Name = product.Name,
                Color = product.Color,
                Cost = product.Cost,
                MainPhotoName = product.MainPhotoName,
                OldCost = product.OldCost,
                ProductStatus = product.ProductStatus,
                ProductPhotos = await _productPhotoRepository.GetPhotosByIdAsync(product.Id)
            };

            return model;
        }

        public async Task<bool> UpdateAsync(ProductUpdateVM model)
        {
            if (!_modelState.IsValid) return false;

            var product = await _productRepository.GetWithPhotosAsync();
            bool hasError = false;


            foreach (var photo in model.Photos)
            {
                if (!_fileService.IsImage(photo))
                {
                    _modelState.AddModelError("ProductPhotos", $"{photo.FileName} yuklediyiniz file sekil formatinda olmalidir");
                    hasError = true;

                }
                else if (!_fileService.CheckSize(photo, 2500))
                {
                    _modelState.AddModelError("ProductPhotos", $"{photo.FileName} ci yuklediyiniz sekil 2500 kb dan az olmalidir");
                    hasError = true;

                }
            }

            if (hasError) { return false; }



            int order = product.ProductPhotos.Count > 0 ? product.ProductPhotos.OrderByDescending(pp => pp.Order).FirstOrDefault().Order : 1;
            foreach (var photo in model.Photos)
            {
                if (photo != null)
                {
                    var productPhoto = new ProductPhoto
                    {
                        
                        PhotoName  = await _fileService.UploadAsync(photo),
                        Order = order++,
                        ProductId = product.Id
                    };
                    await _productPhotoRepository.CreateAsync(productPhoto);
                }

            }


            if (product != null)
            {
                product.Specificity = model.Specificity;
                product.Name = model.Name;
                product.Cost = model.Cost;
                product.OldCost = model.OldCost;
                product.ProductStatus = model.ProductStatus;
                product.Description = model.Description;
                product.Color = model.Color;
                product.ModifiedAt = DateTime.Now;
                if (model.MainPhoto != null)
                {
                    product.MainPhotoName= await _fileService.UploadAsync(model.MainPhoto);
                }
                await _productRepository.UpdateAsync(product);
            }


            return true;
        }

        public async Task<ProductPhotoUpdateVM> GetPhotoUpdateModelAsync(int id)
        {

            var productPhoto = await _productPhotoRepository.GetAsync(id);

            if (productPhoto == null) return null;

            var model = new ProductPhotoUpdateVM
            {
                Id = id,
                Order = productPhoto.Order,
                ProductId = productPhoto.ProductId
            };

            return model;

        }

        public async Task<bool> UpdatePhotoAsync(ProductPhotoUpdateVM model)
        {
            if (!_modelState.IsValid) return false;

            var productPhoto = await _productPhotoRepository.GetAsync(model.ProductId);

            if (productPhoto != null)
            {
                productPhoto.Order = model.Order;

                await _productPhotoRepository.UpdateAsync(productPhoto);
            }
            return true;
        }

        public async Task<bool> AddTagsAsync(ProductAddTagsVM model)
        {
            if (!_modelState.IsValid) return false;

            var product = await _productRepository.GetAsync(model.ProductId);
            if (product == null)
            {
                _modelState.AddModelError("ProductId", "Product tapılmadı");
                return false;
            }

            foreach (var tagId in model.TagsIds)
            {
                var tag = await _tagRepository.GetAsync(tagId);
                if (tag == null)
                {
                    _modelState.AddModelError(string.Empty, $"{tagId} id-li tag tapılmadı");
                    return false;
                }

                var isExist = await _productTagRepository.AnyAsync(ct => ct.ProductId == product.Id && ct.TagId == tagId);
                if (isExist)
                {
                    _modelState.AddModelError(string.Empty, $"{tag.Id} id-li tag artıq bu Product əlavə olunub");
                    return false;
                }

                var productTag = new ProductTag
                {
                    ProductId = product.Id,
                    TagId = tag.Id
                };

                await _productTagRepository.CreateAsync(productTag);
            }

            return true;
        }

        public async Task<ProductAddTagsVM> GetAddTagsModelAsync(int id)
        {
            var product = await _productRepository.GetAsync(id);
            if (product == null) return null;

            var tags = await _tagRepository.GetAllAsync();

            var model = new ProductAddTagsVM
            {
                Tags = tags.Select(t => new SelectListItem
                {
                    Text = t.Title,
                    Value = t.Id.ToString()
                })
                .ToList()
            };

            return model;
        }
    }
}
