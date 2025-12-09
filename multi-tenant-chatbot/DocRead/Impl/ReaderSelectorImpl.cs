using System;
using System.Collections.Generic;
using multi_tenant_chatBot.Dto;

namespace multi_tenant_chatBot.DocRead.Impl;

public class ReaderSelectorImpl:IReaderSelector
{
    Dictionary<ReaderType,IReaders>  _readers = new Dictionary<ReaderType,IReaders>();

    public ReaderSelectorImpl(IEnumerable<IReaders> readers)
    {
        foreach (var reader in readers)
        {
            foreach (ReaderType readerType in Enum.GetValues(typeof(ReaderType)))
            {
                if (reader.CheckType(readerType))
                {
                    _readers.Add(readerType,reader);
                }
            }
        }
    }

    public IReaders GetReader(ReaderType type)
    {
        return _readers[type];
    }
}