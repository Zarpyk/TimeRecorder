﻿using TimeRecorderAPI.Adapter.Out.Persistence.GenericAdapters;
using TimeRecorderAPI.Application.Port.Out.Persistence.TagPort;
using TimeRecorderAPI.Configuration.Adapter;
using TimeRecorderAPI.DB;
using TimeRecorderAPI.Factory;
using TimeRecorderAPI.Models;
using TimeRecorderDomain.DTO;

namespace TimeRecorderAPI.Adapter.Out.Persistence.TagAdapters {
    [PortAdapter(typeof(IAddTagOutPort))]
    public class AddTagOutAdapter(IDataBaseManager db, TagFactory factory)
        : GenericAddOutAdapter<Tag, TagDTO, TagFactory>(db, factory), IAddTagOutPort;
}