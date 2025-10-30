namespace SubsManager.Domain.ValuesObjects
{
    public readonly record struct Money(decimal Amount, string Currency)
    {
        public static Money Zero(string c) => new(0m, c);
        public override string ToString() => $"{Amount:0.00} {Currency}";
    }
}
