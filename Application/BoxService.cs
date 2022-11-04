using System.ComponentModel.DataAnnotations;
using Application.DTOs;
using Application.Interfaces;
using AutoMapper;
using Domain;
using FluentValidation;
using ValidationException = FluentValidation.ValidationException;

namespace Application;

public class BoxService : IBoxService
{
    private readonly IBoxRepository _boxRepository;
    private readonly IValidator<PostBoxDTO> _postValidator;
    private readonly IValidator<Box> _boxValidator;
    private readonly IMapper _mapper;

    public BoxService(
        IBoxRepository repository,
        IValidator<PostBoxDTO> postValidator,
        IValidator<Box> boxValidator,
        IMapper mapper)
    {
        _boxRepository = repository;
        _postValidator = postValidator;
        _boxValidator = boxValidator;
        _mapper = mapper;
    }

    public List<Box> GetAllBoxes()
    {
        return _boxRepository.GetAllBoxes();
    }

    public Box CreateNewBox(PostBoxDTO dto)
    {
        var validation = _postValidator.Validate(dto);
        if (!validation.IsValid)
            throw new
                ValidationException(validation.ToString());

        return _boxRepository.CreateNewBox(_mapper.Map<Box>(dto));
    }

    public Box GetBoxById(int id)
    {
        return _boxRepository.GetBoxById(id);
    }

    public Box UpdateBox(int id, Box box)
    {
        if (id != box.Id)
            throw new
                ValidationException("ID in body and route are different");

        var validation = _boxValidator.Validate(box);
        if (!validation.IsValid)
            throw new
                ValidationException(validation.ToString());

        return _boxRepository.UpdateBox(box);
    }

    public Box DeleteBox(int id)
    {
        return _boxRepository.DeleteBox(id);
    }

    public void RebuildDb()
    {
        _boxRepository.RebuildDb();
    }
}