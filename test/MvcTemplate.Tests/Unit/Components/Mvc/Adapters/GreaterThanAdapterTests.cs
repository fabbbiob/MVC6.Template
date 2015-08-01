﻿using Microsoft.AspNet.Mvc.ModelBinding;
using Microsoft.AspNet.Mvc.ModelBinding.Metadata;
using Microsoft.AspNet.Mvc.ModelBinding.Validation;
using MvcTemplate.Components.Mvc;
using MvcTemplate.Tests.Objects;
using NSubstitute;
using System;
using System.Linq;
using Xunit;

namespace MvcTemplate.Tests.Unit.Components.Mvc
{
    public class GreaterThanAdapterTests
    {
        #region Method: GetClientValidationRules(ClientModelValidationContext context)

        [Fact]
        public void GetClientValidationRules_ReturnsMinRangeValidationRule()
        {
            IModelMetadataProvider provider = new DefaultModelMetadataProvider(Substitute.For<ICompositeMetadataDetailsProvider>());
            ModelMetadata metadata = provider.GetMetadataForProperty(typeof(AdaptersModel), "GreaterThan");
            String errorMessage = new GreaterThanAttribute(128).FormatErrorMessage("GreaterThan");
            GreaterThanAdapter adapter = new GreaterThanAdapter(new GreaterThanAttribute(128));

            ClientModelValidationContext context = new ClientModelValidationContext(metadata, provider, Substitute.For<IServiceProvider>());
            ModelClientValidationRule actual = adapter.GetClientValidationRules(context).Single();

            Assert.Equal(128M, actual.ValidationParameters["min"]);
            Assert.Equal(errorMessage, actual.ErrorMessage);
            Assert.Equal("greater", actual.ValidationType);
            Assert.Single(actual.ValidationParameters);
        }

        #endregion
    }
}