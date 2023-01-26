private string FindMerkleRootHash(IList<Transaction> transactionList)
{
    var transactionStrList = transactionList.Select(tran => CalculateHash(CalculateHash(tran.From + tran.To + tran.Amount))).ToList();
    return BuildMerkleRootHash(transactionStrList);
}

private string BuildMerkleRootHash(IList<string> merkelLeaves)
{
    if (merkelLeaves == null || !merkelLeaves.Any())
        return string.Empty;

    if (merkelLeaves.Count() == 1)
        return merkelLeaves.First();

    if (merkelLeaves.Count() % 2 > 0)
        merkelLeaves.Add(merkelLeaves.Last());

    var merkleBranches = new List<string>();

    for (int i = 0; i < merkelLeaves.Count(); i += 2)
    {
        var leafPair = string.Concat(merkelLeaves[i], merkelLeaves[i + 1]);
        merkleBranches.Add(CalculateHash(CalculateHash(leafPair)));
    }
    return BuildMerkleRootHash(merkleBranches);
}
