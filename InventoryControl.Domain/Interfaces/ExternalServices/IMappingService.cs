﻿namespace InventoryControl.Domain.Interfaces.ExternalServices;

public interface IMappingService
{
    TDestination Map<TDestination>(object source);
    IEnumerable<TDestination> MapRange<TDestination>(IEnumerable<object> source);
    void Map<TSource, TDestination>(TSource source, TDestination destination);
}