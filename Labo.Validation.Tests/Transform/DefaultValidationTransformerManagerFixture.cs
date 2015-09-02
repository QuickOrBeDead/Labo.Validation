namespace Labo.Validation.Tests.Transform
{
    using Labo.Validation.Transform;

    using NSubstitute;

    using NUnit.Framework;

    [TestFixture]
    public class DefaultValidationTransformerManagerFixture
    {
        [Test]
        public void GetValidationTransformerForModelShouldReturnTheRegisteredTransformerForTheSpecifiedType()
        {
            IValidationTransformer validationTransformer = Substitute.For<IValidationTransformer>();
            validationTransformer.UIModelType.Returns(typeof(string));
            validationTransformer.ValidationModelType.Returns(typeof(int));

            DefaultValidationTransformerManager validationTransformerManager = new DefaultValidationTransformerManager();
            validationTransformerManager.RegisterValidationTransformer(validationTransformer);

            Assert.AreSame(validationTransformer, validationTransformerManager.GetValidationTransformerForUIModel(typeof(string)));
        }

        [Test]
        public void GetValidationTransformerForModelShouldReturnNullWhenNoTransformerIsFoundForTheSpecifiedType()
        {
            IValidationTransformer validationTransformer = Substitute.For<IValidationTransformer>();
            validationTransformer.UIModelType.Returns(typeof(string));
            validationTransformer.ValidationModelType.Returns(typeof(int));

            DefaultValidationTransformerManager validationTransformerManager = new DefaultValidationTransformerManager();
            validationTransformerManager.RegisterValidationTransformer(validationTransformer);

            Assert.IsNull(validationTransformerManager.GetValidationTransformerForUIModel(typeof(int)));
        }

        [Test]
        public void GetValidationTransformerForModelShouldReturnNullWhenRegistyIsEmpty()
        {
            DefaultValidationTransformerManager validationTransformerManager = new DefaultValidationTransformerManager();

            Assert.IsNull(validationTransformerManager.GetValidationTransformerForUIModel(typeof(string)));
        }
    }
}
