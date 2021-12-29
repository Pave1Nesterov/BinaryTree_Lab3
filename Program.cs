using System;

namespace BinaryTree
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] tree_A = new int[] { 6, 15, 30, 45, 38, 54 }; //дерево А
            int[] tree_B = new int[] { 15, 2, 10, 20, 39, 54, 38 }; //дерево B
            //2, 6, 10, 15, 20, 30, 39, 41, 45, 54;
            Node root_A;
            root_A = BinarySearchTree.CreateTree(tree_A);
            Node root_B;
            root_B = BinarySearchTree.CreateTree(tree_B);

            //Console.WriteLine("Прямой обход дерева А:");
            //BinarySearchTree.PreOrderTraversal(root_A);
            Console.WriteLine("Обратный обход дерева А:");
            BinarySearchTree.ReverseTraversal(root_A);
            //Console.WriteLine("Симметричный обход дерева А:");
            //BinarySearchTree.SymmetricTraversal(root_A);

            //Console.WriteLine("Прямой обход дерева B:");
            //BinarySearchTree.PreOrderTraversal(root_B);
            //Console.WriteLine("Обратный обход дерева B:");
            //BinarySearchTree.ReverseTraversal(root_B);
            Console.WriteLine("Симметричный обход дерева B:");
            BinarySearchTree.SymmetricTraversal(root_B);

            root_A =  BinarySearchTree.Conjunction(root_A, root_B);
            Console.WriteLine("Прямой обход дерева А после выполнения операции A Uпр B :");
            BinarySearchTree.PreOrderTraversal(root_A);
        }
    }

    class Node
    {
        public int value;
        public Node left;
        public Node right;

        public Node(int value)
        {
            this.value = value;
        }
    }

    class BinarySearchTree
    {
        public static Node CreateTree(int[] tree)
        {
            int n = tree.Length;
            int x = 1;
            Node root;

            if (n % 2 != 0)
            {
                int i = (n - 1) / 2;
                root = new Node(tree[i]);
                for (int k = 0; k < (n - 1) / 2; k++)
                {
                    i -= x;
                    root = BinarySearchTree.Add(root, tree[i]);
                    x++;
                    i += x;
                    root = BinarySearchTree.Add(root, tree[i]);
                    x++;
                }
            }
            else
            {
                int i = n / 2 - 1;
                root = new Node(tree[i]);
                for (int k = 0; k < n / 2 - 1; k++)
                {
                    i -= x;
                    root = BinarySearchTree.Add(root, tree[i]);
                    x++;
                    i += x;
                    root = BinarySearchTree.Add(root, tree[i]);
                    x++;
                }
                root = BinarySearchTree.Add(root, tree[n - 1]);
            }

            return root;
        } //инициализация дерева

        public static Node Search(Node root, int value)
        {
            if (root.value == value)
            {
                return root;
            }
            else if (value < root.value)
            {
                root = Search(root.left, value);
            }
            else if (value > root.value)
            {
                root = Search(root.right, value);
            }
            return root;
        } //поиск узла

        public static Node Add(Node root, int value)
        {
            if (value == root.value)
            {
                return root;
            }
            else if (value < root.value)
            {
                if (root.left != null)
                {
                    root.left = Add(root.left, value);
                }
                else
                {
                    Node newNode = new Node(value);
                    root.left = newNode;
                }
            }
            else
            {
                if (root.right != null)
                {
                    root.right = Add(root.right, value);
                }
                else
                {
                    Node newNode = new Node(value);
                    root.right = newNode;
                }
            }

            return root;
        } //добавление узла

        public static Node Conjunction(Node rootA, Node rootB)
        {
            //прямой обход по дереву В и добавление узлов в дерево А(С), не встречавшихся в дереве А

            rootA = BinarySearchTree.Add(rootA, rootB.value);

            if (rootB.left != null)
            {
                Node rootB_temp = rootB.left;
                rootA = Add(rootA, rootB_temp.value);
                Conjunction(rootA, rootB_temp);
            }
            if (rootB.right != null)
            {
                Node rootB_temp = rootB.right;
                rootA = Add(rootA, rootB_temp.value);
                Conjunction(rootA, rootB_temp);
            }

            return rootA;
        } //A = A Uпр B 

        public static void PreOrderTraversal(Node root) //прямой обход
        {
            Console.WriteLine(root.value);

            if (root.left != null)
            {
                PreOrderTraversal(root.left);
            }
            if (root.right != null)
            {
                PreOrderTraversal(root.right);
            }
        }

        public static void ReverseTraversal(Node root) //обратный обход
        {
            if (root.left != null)
            {
                ReverseTraversal(root.left);
            }
            if (root.right != null)
            {
                ReverseTraversal(root.right);
            }

            Console.WriteLine(root.value);
        }

        public static void SymmetricTraversal(Node root) //симметричный обход
        {
            if (root.left != null)
            {
                SymmetricTraversal(root.left);
            }

            Console.WriteLine(root.value);

            if (root.right != null)
            {
                SymmetricTraversal(root.right);
            }
        }
    }
}