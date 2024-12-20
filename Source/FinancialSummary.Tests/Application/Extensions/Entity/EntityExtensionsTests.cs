namespace FinancialSummary.Tests.Application.Extensions.Entity;

using Categories;
using FinancialSummary.Application.Extensions.Entity;
using FinancialSummary.Domain.Entities.Deposit;
using FluentAssertions;
using TddXt.AnyRoot.Strings;
using static TddXt.AnyRoot.Root;

[Parallelizable, ApplicationLayerTests]
public class EntityExtensionsTests
{
	[Test]
	public void UpdateProperty_IfValueIsNull_DoesNotChangeValueInEntity()
	{
		// Arrange
		DepositEntity depositEntity = Any.Instance<DepositEntity>();
		string oldName = depositEntity.Name;
		
		// Act
		depositEntity.UpdateProperty(x => x.Name, null);

		// Assert
		depositEntity.Name.Should().Be(oldName);
	}
	
	[Test]
	public void UpdateProperty_ChangeValueInEntity()
	{
		// Arrange
		string newName = Any.String();
		DepositEntity depositEntity = Any.Instance<DepositEntity>();
		
		// Act
		depositEntity.UpdateProperty(x => x.Name, newName);

		// Assert
		depositEntity.Name.Should().Be(newName);
	}
	
	[Test]
	public void UpdateProperty_ForStruct_IfValueIsNull_DoesNotChangeValueInEntity()
	{
		// Arrange
		DepositEntity depositEntity = Any.Instance<DepositEntity>();
		int oldCapitalization = depositEntity.CapitalizationPerYear;
		
		// Act
		depositEntity.UpdateProperty(x => x.CapitalizationPerYear, null);

		// Assert
		depositEntity.CapitalizationPerYear.Should().Be(oldCapitalization);
	}
	
	[Test]
	public void UpdateProperty_ForStruct_ChangeValueInEntity()
	{
		// Arrange
		const int newCash = -80;
		DepositEntity depositEntity = Any.Instance<DepositEntity>();
		
		// Act
		depositEntity.UpdateProperty(x => x.Cash, newCash);

		// Assert
		depositEntity.Cash.Should().Be(newCash);
	}
}