# Introduction #

Eft uses CSS selector syntax to locate [Element](Element.md)s of the Windows application with little difference. Here list the supported selector types and the difference with W3C standard selectors.


# Supported Selectors #
## Universal selector ##
```
* 
```
select all elements of the application
## Type selector ##
```
E (eg.Button) 
```
select [Element](Element.md) with control type E

## Attribute selector ##
```
E[foo='content like 'this''] 
  foo := className | name| id
```
select [Element](Element.md) with control type E and attribute foo with the value "content like 'this'".
So far the attribute value can not contain "[".
## Id selector ##
```
#id 
E#id 
E#'id with space'
```
id selector is identical to [id='some id']

## ClassName selector ##
```
.class
E.class
E.'class with space'
```
Class selector is identical to [className='class']

**_Notice:_ The value of id and class selector can not contain single quotes, if they do contain single quotes, use attribute selector syntax instead.**

## Structural pseudo class ##
```
E:nth-of-type(n)
E:first-of-type
E:last-of-type
```

## Combination Selectors ##
```
E F
E > F
```

# Differences with standard W#C CSS selector #
In eft:
  * id and className can contain space and maybe other characters because the id and class name of window and control may contain space and other characters.
  * Multiple controls can have same id(which is actually automation id).

