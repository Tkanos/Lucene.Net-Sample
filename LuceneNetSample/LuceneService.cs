using Lucene.Net.Analysis;
using Lucene.Net.Analysis.Standard;
using Lucene.Net.Documents;
using Lucene.Net.Index;
using Lucene.Net.QueryParsers;
using Lucene.Net.Search;
using Lucene.Net.Store;
using System.Collections.Generic;

namespace LuceneNetSample
{
    public class LuceneService
    {
        private Analyzer _analyzer = new StandardAnalyzer(Lucene.Net.Util.Version.LUCENE_29);
        private Directory _luceneIndexDirectory;
        private IndexWriter _writer;
        private string _indexPath = @"D:\Index"; //change this value to feet your needs

        public LuceneService()
        {
            /*
             * Change this value to choose where  you want to store your lucene data
             * True = in a  Directory on your Hard Disk
             * False = on the RAM
             */
            var usingDirectory = false;

            if (usingDirectory)
            {
                if (System.IO.Directory.Exists(_indexPath))
                {
                    System.IO.Directory.Delete(_indexPath, true);
                }

                _luceneIndexDirectory = FSDirectory.Open(new System.IO.DirectoryInfo(_indexPath));
            }
            else
            {
                _luceneIndexDirectory = new RAMDirectory();
            }

            _writer = new IndexWriter(_luceneIndexDirectory, _analyzer, true, IndexWriter.MaxFieldLength.UNLIMITED);

            var data = MyModel.Load();
            BuildIndex(data);
        }

        private void BuildIndex(IList<MyModel> data)
        {
            foreach (var index in data)
            {
                Document doc = new Document();
                doc.Add(new Field(MyModel.Mapping.Id, index.Id.ToString(), Field.Store.YES, Field.Index.NOT_ANALYZED));
                doc.Add(new Field(MyModel.Mapping.Age, index.Age.ToString(), Field.Store.YES, Field.Index.NOT_ANALYZED));
                doc.Add(new Field(MyModel.Mapping.Name, index.Name, Field.Store.YES, Field.Index.ANALYZED, Field.TermVector.YES));

                _writer.AddDocument(doc);
            }
            _writer.Optimize();
            _writer.Dispose();
        }

        public IList<MyModel> Search(string searchTerm)
        {
            //initialize the searcher
            Searcher searcher = new IndexSearcher(_luceneIndexDirectory, true);//true opens the index in read only mode

            //creates the query
            QueryParser parser = new QueryParser(Lucene.Net.Util.Version.LUCENE_29, MyModel.Mapping.Name, _analyzer);
            Query query = parser.Parse(searchTerm); //it will create the request name:{searchTerm}

            //Execute Query
            TopScoreDocCollector collection = TopScoreDocCollector.Create(100, true);
            searcher.Search(query, collection);
            ScoreDoc[] hits = collection.TopDocs().ScoreDocs;

            //GetResult
            var result = new List<MyModel>();
            for (int i = 0; i < hits.Length; i++)
            {
                int docId = hits[i].Doc;
                float score = hits[i].Score;
                Document doc = searcher.Doc(docId);

                result.Add(new MyModel
                {
                    Id = int.Parse(doc.Get(MyModel.Mapping.Id)),
                    Age = int.Parse(doc.Get(MyModel.Mapping.Age)),
                    Name = doc.Get(MyModel.Mapping.Name),
                });
            }

            return result;
        }
    }
}
