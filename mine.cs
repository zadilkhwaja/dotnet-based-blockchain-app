private void GenerateBlock()
{
        var lastBlock = Blockchain.LastOrDefault();
        var block = new Block()
        {
                TimeStamp = DateTime.Now,
                Nounce = 0,
                TransactionList = TransactionPool.TakeAll(),
                Index = (lastBlock?.Index + 1 ?? 0),
                PrevHash = lastBlock?.Hash ?? string.Empty
        };
        MineBlock(block);
        Blockchain.Add(block);
}

private void MineBlock(Block block)
{
        var merkleRootHash = FindMerkleRootHash(block.TransactionList);
        long nounce = -1;
        var hash = string.Empty;
        do
        {
                nounce++;
                var rowData = block.Index + block.PrevHash + block.TimeStamp.ToString() + nounce + merkleRootHash;
                hash = CalculateHash(CalculateHash(rowData));
        }
        while (!hash.StartsWith("0000"));
        block.Hash = hash;
        block.Nounce = nounce;
}
