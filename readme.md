# C# 7.x Features

## Particularly looking at Tuple Equality, with a ValueTuple

```
    return (p1.FirstName, p1.LastName, p1.MiddleName) == (p2.FirstName, p2.LastName, p2.MiddleName);
```

and using tuples to GetHashCode
```
    return (FirstName, LastName, MiddleName).GetHashCode();
```