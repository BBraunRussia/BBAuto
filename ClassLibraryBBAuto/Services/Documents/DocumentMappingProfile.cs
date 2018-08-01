using AutoMapper;
using BBAuto.Repository.Models;

namespace BBAuto.Domain.Services.Documents
{
  public class DocumentMappingProfile : Profile
  {
    public DocumentMappingProfile()
    {
      CreateMap<DbDocument, Document>().ReverseMap();
    }
  }
}
