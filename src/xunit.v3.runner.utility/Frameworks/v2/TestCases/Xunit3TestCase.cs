﻿using System;
using System.Collections.Generic;
using Xunit.Abstractions;
using Xunit.Internal;
using Xunit.v3;

namespace Xunit.Runner.v2
{
	/// <summary>
	/// Provides a class which wraps <see cref="ITestCase"/> instances to implement <see cref="_ITestCase"/>.
	/// </summary>
	public class Xunit3TestCase : _ITestCase
	{
		readonly Lazy<_ITestMethod> testMethod;

		/// <summary>
		/// Initializes a new instance of the <see cref="Xunit3TestCase"/> class.
		/// </summary>
		/// <param name="v2TestCase">The v2 test case to wrap</param>
		public Xunit3TestCase(ITestCase v2TestCase)
		{
			V2TestCase = Guard.ArgumentNotNull(nameof(v2TestCase), v2TestCase);

			testMethod = new Lazy<_ITestMethod>(() => new Xunit3TestMethod(V2TestCase.TestMethod));
		}

		/// <inheritdoc/>
		public string DisplayName => V2TestCase.DisplayName;

		/// <inheritdoc/>
		public string? SkipReason => V2TestCase.SkipReason;

		/// <inheritdoc/>
		public _ISourceInformation? SourceInformation
		{
			get => new _SourceInformation { FileName = V2TestCase.SourceInformation?.FileName, LineNumber = V2TestCase.SourceInformation?.LineNumber };
			set => V2TestCase.SourceInformation = new SourceInformation { FileName = value?.FileName, LineNumber = value?.LineNumber };
		}

		/// <inheritdoc/>
		public _ITestMethod TestMethod => testMethod.Value;

		/// <inheritdoc/>
		public object?[]? TestMethodArguments => V2TestCase.TestMethodArguments;

		/// <inheritdoc/>
		public Dictionary<string, List<string>> Traits => V2TestCase.Traits;

		/// <inheritdoc/>
		public string UniqueID => V2TestCase.UniqueID;

		/// <summary>
		/// Gets the underlying xUnit.net v2 <see cref="ITestCase"/> that this class is wrapping.
		/// </summary>
		public ITestCase V2TestCase { get; }
	}
}
