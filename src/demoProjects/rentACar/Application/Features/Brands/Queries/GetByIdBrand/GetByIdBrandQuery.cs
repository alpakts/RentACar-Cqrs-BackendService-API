using Application.Features.Brands.Dtos;
using Application.Features.Brands.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Brands.Queries.GetByIdBrand
{
    public  class GetByIdBrandQuery:IRequest<BrandGetByIdDto>
    {
        public int Id { get; set; }
        public class GetByIdBrandHander:IRequestHandler<GetByIdBrandQuery,BrandGetByIdDto>
        {
            private readonly IBrandRepository _brandRepository;
            private readonly IMapper _mapper;
            BrandBusinessRules _rules;

            public GetByIdBrandHander(IBrandRepository brandRepository, IMapper mapper,BrandBusinessRules rules)
            {
                _brandRepository = brandRepository;
                _mapper = mapper;
                _rules = rules;
            }

            public async Task<BrandGetByIdDto> Handle(GetByIdBrandQuery request, CancellationToken cancellationToken)
            {

                Brand? brand = await _brandRepository.GetAsync(b => b.Id == request.Id );
                _rules.BrandShouldExistWhenRequested(brand);
                BrandGetByIdDto mappedBrand=_mapper.Map<BrandGetByIdDto>(brand);

                return mappedBrand;
            }
        }
    }
}
