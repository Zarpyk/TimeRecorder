﻿using TimeRecorderAPI.Application.Port.In.Service.GenericPort;
using TimeRecorderDomain.DTO;

namespace TimeRecorderAPI.Application.Port.In.Service.TagPort {
    public interface IDeleteTagInPort : IGenericDeleteInPort<TagDTO>;
}