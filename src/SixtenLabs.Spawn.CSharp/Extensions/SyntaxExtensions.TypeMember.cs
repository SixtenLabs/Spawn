using System.Collections.Generic;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using SF = Microsoft.CodeAnalysis.CSharp.SyntaxFactory;
using Microsoft.CodeAnalysis.CSharp;
using System;

namespace SixtenLabs.Spawn.CSharp
{
	public static partial class SyntaxExtensions
	{
		private static EqualsValueClauseSyntax GetInitializer(LiteralDefinition literalDefinition)
		{
			EqualsValueClauseSyntax equalsSyntax = null;

			if (literalDefinition != null)
			{
				var expression = GetLiteralExpression(literalDefinition);
				equalsSyntax = SF.EqualsValueClause(expression);
			}

			return equalsSyntax;
		}

		public static ConstructorDeclarationSyntax AddConstructor(ConstructorDefinition constructorDefinition)
		{
			var modifiers = GetModifierTokens(constructorDefinition.ModifierDefinitions);

			var returnTypeString = constructorDefinition.TranslatedReturnType;
			var attributes = GetAttributes(constructorDefinition.Attributes);
			var parameters = GetParameters(constructorDefinition.Parameters);
			var body = CreateBlock(constructorDefinition.Block);

			var constructorDeclaration = SF.ConstructorDeclaration(SF.Identifier(constructorDefinition.TranslatedName))
				.WithModifiers(modifiers)
				.WithAttributeLists(SF.SingletonList(attributes))
				.WithParameterList(parameters)
				.WithBody(body);

			return constructorDeclaration;
		}

		public static IList<ConstructorDeclarationSyntax> AddConstructors(IList<ConstructorDefinition> constructorDefinitions)
		{
			var list = new List<ConstructorDeclarationSyntax>();

			foreach (var constructorDefinition in constructorDefinitions)
			{
				var constructorDeclarationSyntax = AddConstructor(constructorDefinition);
				list.Add(constructorDeclarationSyntax);
			}

			return list;
		}

		public static FieldDeclarationSyntax AddField(FieldDefinition fieldDefinition)
		{
			if(string.IsNullOrEmpty(fieldDefinition.TranslatedReturnType))
			{
				throw new ArgumentNullException("Field must have a return type.");
			}

			var modifiers = GetModifierTokens(fieldDefinition.ModifierDefinitions);

			var returnTypeString = fieldDefinition.TranslatedReturnType;
			var returnType = SF.VariableDeclaration(SF.IdentifierName(returnTypeString));
			var fieldName = SF.VariableDeclarator(SF.Identifier(fieldDefinition.TranslatedName));
			var initializer = GetInitializer(fieldDefinition.DefaultValue);
			var attributes = GetAttributes(fieldDefinition.Attributes);

			fieldName = fieldName.WithInitializer(initializer);

			var fieldDeclaration = SF.FieldDeclaration(returnType
				.AddVariables(fieldName))
				.WithModifiers(modifiers);

			if(fieldDefinition.Attributes.Count > 0)
			{
				fieldDeclaration = fieldDeclaration.WithAttributeLists(SF.SingletonList(attributes));
			}

			return fieldDeclaration;
		}

		public static IList<FieldDeclarationSyntax> AddFields(IList<FieldDefinition> fieldDefinitions)
		{
			var list = new List<FieldDeclarationSyntax>();

			foreach (var fieldDefinition in fieldDefinitions)
			{
				var fieldDeclarationSyntax = AddField(fieldDefinition);
				list.Add(fieldDeclarationSyntax);
			}

			return list;
		}

		private static AccessorDeclarationSyntax ComposeAccessor(AccessorDefinition definition)
		{
			var declaration = SF.AccessorDeclaration(definition.AccessorType);

			if (definition.Modifier != SyntaxKind.None)
			{
				declaration = declaration.WithModifiers(SF.TokenList(SF.Token(definition.Modifier)));
			}

			if (definition.Block != null)
			{
				var block = CreateBlock(definition.Block);
				declaration = declaration.WithBody(block);
			}
			else
			{
				declaration = declaration
				.WithSemicolonToken(SF.Token(SyntaxKind.SemicolonToken));
			}

			return declaration;
		}

		private static AccessorListSyntax GetAccessors(AccessorDefinition getter, AccessorDefinition setter)
		{
			var accessorList = SF.AccessorList();

			if (getter != null)
			{
				accessorList = accessorList.AddAccessors(ComposeAccessor(getter));
			}

			if (setter != null)
			{
				accessorList = accessorList.AddAccessors(ComposeAccessor(setter));
			}

			return accessorList;
		}

		public static PropertyDeclarationSyntax AddProperty(PropertyDefinition propertyDefinition)
		{
			var modifiers = GetModifierTokens(propertyDefinition.ModifierDefinitions);
			var returnType = SF.ParseTypeName(propertyDefinition.TranslatedReturnType);
			var accessors = GetAccessors(propertyDefinition.Getter, propertyDefinition.Setter);
			var initializer = GetInitializer(propertyDefinition.DefaultValue);

			var declaration = SF.PropertyDeclaration(returnType, SF.Identifier(propertyDefinition.TranslatedName))
				.WithModifiers(modifiers)
				.WithAccessorList(accessors)
				.WithInitializer(initializer);

			return declaration;
		}

		public static IList<PropertyDeclarationSyntax> AddProperties(IList<PropertyDefinition> definitions)
		{
			var list = new List<PropertyDeclarationSyntax>();

			foreach (var property in definitions)
			{
				var propertySyntax = AddProperty(property);
				list.Add(propertySyntax);
			}

			return list;
		}

		public static ParameterSyntax AddParameter(ParameterDefinition definition)
		{
			var typeName = SF.IdentifierName(definition.TranslatedReturnType);

			var token = SF.VerbatimIdentifier(SF.TriviaList(), definition.TranslatedName, "test", SF.TriviaList());

			TypeSyntax typeSyntax = null;

			if (definition.IsPointer)
			{
				// PointerType is hard code here. Need to determine the type from the definition and create appropriate type.
				typeSyntax = SF.PointerType(SF.ParseTypeName(definition.TranslatedReturnType));
			}
			else
			{
				typeSyntax = SF.ParseTypeName(definition.TranslatedReturnType);
			}

			var parameter = SF.Parameter(token).WithType(typeSyntax);

			return parameter;
		}

		private static ParameterListSyntax GetParameters(IList<ParameterDefinition> definitions)
		{
			var list = new List<ParameterSyntax>();

			foreach (var parameter in definitions)
			{
				var parameterSyntax = AddParameter(parameter);
				list.Add(parameterSyntax);
			}

			return SF.ParameterList(SF.SeparatedList(list));
		}

		public static AttributeSyntax AddAttribute(AttributeDefinition attributeDefinition)
		{
			var arguments = new List<AttributeArgumentSyntax>();

			foreach (var arg in attributeDefinition.ArgumentList)
			{
				var argument = SF.AttributeArgument(SF.IdentifierName(arg));
				arguments.Add(argument);
			}

			var attributeArgList = SF.AttributeArgumentList(SF.SeparatedList(arguments));

			var declaration = SF.Attribute(SF.IdentifierName(attributeDefinition.TranslatedName), attributeArgList);

			return declaration;
		}

		private static AttributeListSyntax GetAttributes(IList<AttributeDefinition> definitions)
		{
			var list = new List<AttributeSyntax>();

			foreach (var attribute in definitions)
			{
				var attributeSyntax = AddAttribute(attribute);
				list.Add(attributeSyntax);
			}

			return SF.AttributeList(SF.SeparatedList(list));
		}

		public static MethodDeclarationSyntax AddMethod(MethodDefinition methodDefinition)
		{
			var attributes = GetAttributes(methodDefinition.Attributes);
			var modifiers = GetModifierTokens(methodDefinition.ModifierDefinitions);
			var returnType = SF.ParseTypeName(methodDefinition.TranslatedReturnType);
			var parameters = GetParameters(methodDefinition.Parameters);
			
			var declaration = SF.MethodDeclaration(returnType, SF.Identifier(methodDefinition.TranslatedName))
				.WithAttributeLists(SF.SingletonList(attributes))
				.WithModifiers(modifiers)
				.WithParameterList(parameters)
				.WithSemicolonToken(SF.Token(SyntaxKind.SemicolonToken));

			return declaration;
		}

		public static IList<MethodDeclarationSyntax> AddMethods(IList<MethodDefinition> definitions)
		{
			var list = new List<MethodDeclarationSyntax>();

			foreach (var method in definitions)
			{
				var methodSyntax = AddMethod(method);
				list.Add(methodSyntax);
			}

			return list;
		}
	}
}
