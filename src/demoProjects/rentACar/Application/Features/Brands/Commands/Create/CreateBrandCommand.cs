using Application.Features.Brands.Dtos;
using Application.Features.Brands.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Brands.Commands.Create
{
    public class CreateBrandCommand : IRequest<CreatedBrandDto>
    {
        public string Name { get; set; }

        public class CreateBrandCommandHandler : IRequestHandler<CreateBrandCommand, CreatedBrandDto>
        {
            private readonly IBrandRepository _brandRepository;
            private readonly IMapper _mapper;
            private readonly BrandBusinessRules _rule;

            public CreateBrandCommandHandler(IBrandRepository brandRepository, IMapper mapper,BrandBusinessRules rules)
            {
                _brandRepository = brandRepository;
                _mapper = mapper;
                _rule = rules;
            }

            public async Task<CreatedBrandDto> Handle(CreateBrandCommand request, CancellationToken cancellationToken)
            {
                await _rule.BrandNameCanNotBeDuplicatedWhenInserted(request.Name);
                Brand mappedBrand = _mapper.Map<Brand>(request);
                Brand CreatedBrand= await _brandRepository.AddAsync(mappedBrand);
                CreatedBrandDto createdBrandDto = _mapper.Map<CreatedBrandDto>(CreatedBrand);
                // CreatedBrandDto createdBrandDto = new CreatedBrandDt();
                // createdBrandDto.Name=mappedBrand.Name;
                //Bu şekilde maplerde farklı isimlendirmedeki fieldlar eşleştirilebilir
                return createdBrandDto;

                

            }
         //dto=>id,name
         //brand=>id,name, ileride belki x
            


        }
    }
}

