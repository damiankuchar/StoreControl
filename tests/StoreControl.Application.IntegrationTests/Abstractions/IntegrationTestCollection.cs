﻿namespace StoreControl.Application.IntegrationTests.Abstractions
{
    [CollectionDefinition(nameof(IntegrationTestCollection))]
    public class IntegrationTestCollection : ICollectionFixture<IntegrationTestWebAppFactory>
    {
    }
}
