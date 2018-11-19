using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace RoboBackup {
    /// <summary>
    /// Node traversal helper class for <see cref="TaskForm"/>'s <see cref="TreeView"/> object instance.
    /// </summary>
    public static class DirectoryTree {
        /// <summary>
        /// Locates and returns <see cref="TreeNode"/> with given full path within a <see cref="TreeNodeCollection"/>.
        /// </summary>
        /// <param name="nodes"><see cref="TreeNodeCollection"/> to search in.</param>
        /// <param name="fullPath">Full node path of the searched <see cref="TreeNode"/>.</param>
        /// <returns><see cref="TreeNode"/> with given full path</returns>
        public static TreeNode FindNode(TreeNodeCollection nodes, string fullPath) {
            foreach (TreeNode child in nodes) {
                if (child.FullPath == fullPath) {
                    return child;
                }
                TreeNode match = FindNode(child.Nodes, fullPath);
                if (match != null) {
                    return match;
                }
            }
            return null;
        }

        /// <summary>
        /// Locates and returns collection of nodes with unchecked checkbox within a <see cref="TreeNodeCollection"/>.
        /// </summary>
        /// <param name="nodes"><see cref="TreeNodeCollection"/> to search in.</param>
        /// <returns><see cref="List"/> of <see cref="TreeNode"/>s with unchecked checkbox.</returns>
        public static List<TreeNode> GetExcludedNodes(TreeNodeCollection nodes) {
            List<TreeNode> list = new List<TreeNode>();
            foreach (TreeNode child in nodes) {
                if (!child.Checked) {
                    list.Add(child);
                } else {
                    list.AddRange(GetExcludedNodes(child.Nodes));
                }
            }
            return list;
        }

        /// <summary>
        /// Traverses given filesystem path and creates an array of <see cref="TreeNode"/>s corresponding to filesystem objects under that path. 
        /// </summary>
        /// <param name="path">Filesystem path to traverse.</param>
        /// <returns>Array of <see cref="TreeNode"/>s corresponding to filesystem objects found under given path</returns>
        public static TreeNode[] LoadNodes(string path) {
            List<TreeNode> nodes = new List<TreeNode>();
            IEnumerable<string> directories = new string[] { };
            IEnumerable<string> files = new string[] { };
            try {
                directories = Directory.EnumerateDirectories(path);
                files = Directory.EnumerateFiles(path);
            } catch { /* Ignore if the subdirectory is inaccessible */ }
            foreach (string dir in directories) {
                TreeNode node = new TreeNode(Path.GetFileName(dir)) { Checked = true, Tag = EntryType.Directory };
                node.Nodes.AddRange(LoadNodes(dir));
                nodes.Add(node);
            }
            foreach (string file in files) {
                nodes.Add(new TreeNode(Path.GetFileName(file)) { Checked = true, Tag = EntryType.File });
            }
            return nodes.ToArray();
        }

        /// <summary>
        /// Checks or unchecks checkboxes for all <see cref="TreeNode.Nodes"/> of the given <see cref="TreeNode"/>.
        /// </summary>
        /// <param name="node"><see cref="TreeNode"/> to descend to.</param>
        /// <param name="check"><see cref="TreeNode.Checked"/> state to set.</param>
        public static void ToggleChildren(TreeNode node, bool check) {
            foreach (TreeNode child in node.Nodes) {
                child.Checked = check;
                ToggleChildren(child, check);
            }
        }
    }
}
