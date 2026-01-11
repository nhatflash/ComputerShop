using ComputerShop.Repository.CustomExceptions;
using ComputerShop.Repository.Models;
using ComputerShop.Repository.Repositories;

namespace ComputerShop.Service.Services;

public class ManufacturerService
{

    private readonly UnitOfWork _unitOfWork;

    public ManufacturerService(UnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Manufacturer> HandleAddManufacturer(string name)
    {
        if (await _unitOfWork.ManufacturerRepository.ManufacturerExistByNameAsync(name))
        {
            throw new BadRequestException("This manufacturer name is already in use.");
        }
        var manufacturer = new Manufacturer
        {
            Id = Guid.NewGuid(),
            Name = name,
        };
        await _unitOfWork.ManufacturerRepository.AddManufacturerAsync(manufacturer);
        return manufacturer;
    }
}
