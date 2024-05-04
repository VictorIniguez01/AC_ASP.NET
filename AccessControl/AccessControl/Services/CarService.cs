using AccessControl.DTOs;
using Repository.Models;
using Repository.Repository;
using AutoMapper;

namespace AccessControl.Services
{
    public class CarService : ICommonService<CarDto, CarInsertDto, CarUpdateDto>
    {
        private IRepository<Car> _carRepository;
        private IMapper _mapper;
        public CarService(IRepository<Car> carRepository,
                          IMapper mapper)
        {
            _carRepository = carRepository;
            _mapper = mapper;
        }

        public async Task<CarDto> Add(CarInsertDto insertDto)
        {
            Car car = _mapper.Map<Car>(insertDto);

            await _carRepository.Add(car);
            await _carRepository.Save();

            return _mapper.Map<CarDto>(car);
        }

        public async Task<CarDto> Delete(int id)
        {
            Car car = await _carRepository.GetById(id);
            if (car == null)
                return null;

            CarDto carDto = _mapper.Map<CarDto>(car);

            _carRepository.Delete(car);
            await _carRepository.Save();

            return carDto;
        }

        public async Task<IEnumerable<CarDto>> Get()
        {
            IEnumerable<Car> cars = await _carRepository.Get();

            return cars.Select(c => _mapper.Map<CarDto>(c));
        }

        public async Task<CarDto> GetById(int id)
        {
            Car car = await _carRepository.GetById(id);
            if (car == null)
                return null;

            return _mapper.Map<CarDto>(car);
        }

        public async Task<CarDto> Update(int id, CarUpdateDto updateDto)
        {
            Car car = await _carRepository.GetById(id);
            if (car == null)
                return null;

            car = _mapper.Map<CarUpdateDto, Car>(updateDto ,car);

            _carRepository.Update(car);
            await _carRepository.Save();

            return _mapper.Map<CarDto>(car);
        }
    }
}
