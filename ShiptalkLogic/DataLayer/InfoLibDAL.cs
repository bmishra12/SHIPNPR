using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

using ShiptalkLogic.BusinessObjects;

namespace ShiptalkLogic.DataLayer
{
    internal class InfoLibDAL : DALBase
    {

        public bool AddInfoLibItem(InfoLibItem item, out int InfoLibItemId, out string FailureReason)
        {
            if (item == null)
                throw new ArgumentNullException("item");

            using (var command = database.GetStoredProcCommand(StoredProcNames.InfoLib.AddInfoLibItem.Description()))
            {
                //database.AddInParameter(command, "@ItemType", DbType.Int16, item.ItemType);
                database.AddInParameter(command, "@ScopeId", DbType.Int32, item.ViewerScope.HasValue ? item.ViewerScope.Value.EnumValue<int>() : (int?)null);
                database.AddInParameter(command, "@ParentId", DbType.Int32, item.ParentId);
                database.AddInParameter(command, "@HeaderType", DbType.Int16, item.ItemHeader.HeaderType);
                database.AddInParameter(command, "@HeaderText", DbType.String, item.ItemHeader.HeaderText);
                database.AddInParameter(command, "@HeaderGraphics", DbType.Binary, item.ItemHeader.ItemFile);
                database.AddInParameter(command, "@HeaderGraphicsFileExtension", DbType.StringFixedLength, item.ItemHeader.ItemFileExtension);
                database.AddInParameter(command, "@HeaderGraphicsMimeType", DbType.StringFixedLength, item.ItemHeader.ItemFileMimeType);
                database.AddInParameter(command, "@HeaderGraphicsFileSizeInBytes", DbType.Int32, item.ItemHeader.ItemFileSizeInBytes);
                database.AddInParameter(command, "@HeaderGraphicsFileName", DbType.String, item.ItemHeader.ItemFileName);
                database.AddInParameter(command, "@LinkType", DbType.Int16, item.ItemLink.LinkType);
                database.AddInParameter(command, "@LinkUrl", DbType.String, item.ItemLink.LinkUrl);
                database.AddInParameter(command, "@LinkFile", DbType.Binary, item.ItemLink.ItemFile);
                database.AddInParameter(command, "@LinkFileExtension", DbType.StringFixedLength, item.ItemLink.ItemFileExtension);
                database.AddInParameter(command, "@LinkFileMimeType", DbType.StringFixedLength, item.ItemLink.ItemFileMimeType);
                database.AddInParameter(command, "@LinkFileSizeInBytes", DbType.Int32, item.ItemLink.ItemFileSizeInBytes);
                database.AddInParameter(command, "@LinkFileName", DbType.String, item.ItemLink.ItemFileName);
                database.AddInParameter(command, "@OpenLinkInNewWindow", DbType.Boolean, item.ItemLink.OpenLinkInNewWindow);

                database.AddInParameter(command, "@CreatedBy", DbType.Int32, item.CreatedBy);
                database.AddInParameter(command, "@SpecialIdentifier", DbType.Int32, item.SpecialIdentifier);

                database.AddOutParameter(command, "@InfoLibItemID", DbType.Int32, 4);
                database.AddOutParameter(command, "@FailureReason", DbType.String, 500);
                int RecAffected = database.ExecuteNonQuery(command);

                //Initialize output values
                InfoLibItemId = 0;
                FailureReason = string.Empty;

                if (RecAffected > 0)
                    InfoLibItemId = (int)database.GetParameterValue(command, "@InfoLibItemID");
                else
                    FailureReason = database.GetParameterValue(command, "@FailureReason") as string;
                
                return RecAffected > 0;
            }

        }


        public bool UpdateInfoLibItem(InfoLibItem item, out string FailureReason)
        {
            if (item == null)
                throw new ArgumentNullException("item");

            using (var command = database.GetStoredProcCommand(StoredProcNames.InfoLib.UpdateInfoLibItem.Description()))
            {
                database.AddInParameter(command, "@InfoLibItemID", DbType.Int32, item.InfoLibItemId);
                database.AddInParameter(command, "@ScopeId", DbType.Int32, item.ViewerScope.HasValue ? item.ViewerScope.Value.EnumValue<Int32>() : (Int32?)null);
                database.AddInParameter(command, "@ParentId", DbType.Int32, item.ParentId);
                database.AddInParameter(command, "@HeaderType", DbType.Int16, item.ItemHeader.HeaderType);
                database.AddInParameter(command, "@HeaderText", DbType.String, item.ItemHeader.HeaderText);
                database.AddInParameter(command, "@HeaderGraphics", DbType.Binary, item.ItemHeader.ItemFile);
                database.AddInParameter(command, "@HeaderGraphicsFileExtension", DbType.StringFixedLength, item.ItemHeader.ItemFileExtension);
                database.AddInParameter(command, "@HeaderGraphicsMimeType", DbType.StringFixedLength, item.ItemHeader.ItemFileMimeType);
                database.AddInParameter(command, "@HeaderGraphicsFileSizeInBytes", DbType.Int32, item.ItemHeader.ItemFileSizeInBytes);
                database.AddInParameter(command, "@HeaderGraphicsFileName", DbType.String, item.ItemHeader.ItemFileName);
                database.AddInParameter(command, "@LinkType", DbType.Int16, item.ItemLink.LinkType);
                database.AddInParameter(command, "@LinkUrl", DbType.String, item.ItemLink.LinkUrl);
                database.AddInParameter(command, "@LinkFile", DbType.Binary, item.ItemLink.ItemFile);
                database.AddInParameter(command, "@LinkFileExtension", DbType.StringFixedLength, item.ItemLink.ItemFileExtension);
                database.AddInParameter(command, "@LinkFileMimeType", DbType.StringFixedLength, item.ItemLink.ItemFileMimeType);
                database.AddInParameter(command, "@LinkFileSizeInBytes", DbType.Int32, item.ItemLink.ItemFileSizeInBytes);
                database.AddInParameter(command, "@LinkFileName", DbType.String, item.ItemLink.ItemFileName);
                database.AddInParameter(command, "@OpenLinkInNewWindow", DbType.Boolean, item.ItemLink.OpenLinkInNewWindow);
                database.AddInParameter(command, "@LastUpdatedBy", DbType.Int32, item.LastUpdatedBy);
                database.AddInParameter(command, "@IsArchived", DbType.Boolean, item.IsArchived);
                database.AddInParameter(command, "@SpecialIdentifier", DbType.Int32, item.SpecialIdentifier);

                database.AddOutParameter(command, "@FailureReason", DbType.String, 500);

                int RecAffected = database.ExecuteNonQuery(command);

                //Initialize output values
                FailureReason = string.Empty;

                if (RecAffected == -1)
                    FailureReason = database.GetParameterValue(command, "@FailureReason") as string;

                return RecAffected > 0;
            }

        }


        public IEnumerable<InfoLibItem> GetInfoLibItems(Scope? ScopeId, int ParentId)
        {
            using (var command = database.GetStoredProcCommand(StoredProcNames.InfoLib.GetInfoLibItems.Description()))
            {

                IList<InfoLibItem> items = null;

                database.AddInParameter(command, "@ScopeId", DbType.Int32, ScopeId.HasValue ? ScopeId.Value.EnumValue<int>() : (int?)null);
                database.AddInParameter(command, "@ParentId", DbType.Int32, ParentId);
                using (IDataReader reader = database.ExecuteReader(command))
                {
                    InfoLibItem item = null;
                    while (reader.Read())
                    {
                        if (items == null) items = new List<InfoLibItem>();
                        item = new InfoLibItem
                        {
                            InfoLibItemId = reader.GetInt32(0),
                            ParentId = reader.GetInt32(1),
                            ItemHeader = new InfoLibItem.InfoLibItemHeader
                            {
                                HeaderText = reader.IsDBNull(2) ? string.Empty : reader.GetString(2),
                                HeaderType = reader.GetInt16(3).ToEnumObject<InfoLibHeaderType>()
                            },
                            IsArchived = reader.GetBoolean(7),
                            SpecialIdentifier = reader.IsDBNull(8) ? (InfoLibSpecialIdentifiers?)null : reader.GetInt32(8).ToEnumObject<InfoLibSpecialIdentifiers>(),
                             ViewerScope = reader.IsDBNull(9) ? (Scope?)null : reader.GetInt16(9).ToEnumObject<Scope>()
                        };

                        if (!reader.IsDBNull(4))
                        {
                            item.ItemLink = new InfoLibItem.InfoLibItemLink
                             {
                                 LinkType = reader.IsDBNull(4) ? null : (InfoLibLinkType?)reader.GetInt16(4).ToEnumObject<InfoLibHeaderType>(),
                                 LinkUrl = reader.IsDBNull(5) ? string.Empty : reader.GetString(5),
                                 OpenLinkInNewWindow = reader.GetBoolean(6)
                             };
                        };

                        items.Add(item);
                    };

                }

                return items;
                
            }
        }


        public IEnumerable<InfoLibItem> GetTopLevelInfoLibItems(bool IsArchived)
        {
            using (var command = database.GetStoredProcCommand(StoredProcNames.InfoLib.GetTopLevelInfoLibItems.Description()))
            {
                IList<InfoLibItem> items = null;

                database.AddInParameter(command, "@IsArchived", DbType.Boolean, IsArchived);
                using (IDataReader reader = database.ExecuteReader(command))
                {
                    InfoLibItem item = null;
                    while (reader.Read())
                    {
                        if (items == null) items = new List<InfoLibItem>();
                        item = new InfoLibItem
                        {
                            InfoLibItemId = reader.GetInt32(0),
                            ItemHeader = new InfoLibItem.InfoLibItemHeader
                            {
                                HeaderText = reader.IsDBNull(1) ? string.Empty : reader.GetString(1),
                                HeaderType = reader.GetInt16(2).ToEnumObject<InfoLibHeaderType>()
                            },
                            SpecialIdentifier = reader.IsDBNull(3) ? (InfoLibSpecialIdentifiers?)null : reader.GetInt32(3).ToEnumObject<InfoLibSpecialIdentifiers>()
                        };

                        items.Add(item);
                    };
                }

                return items;
            }
        }


        public InfoLibItem GetInfoLibItem(int InfoLibItemId, bool IncludeInfoLibItemLinkFile)
        {
            using (var command = database.GetStoredProcCommand(StoredProcNames.InfoLib.GetInfoLibItem.Description()))
            {

                InfoLibItem item = null;

                database.AddInParameter(command, "@InfoLibItemId", DbType.Int32, InfoLibItemId);
                database.AddInParameter(command, "@IncludeLinkFileAlso", DbType.Boolean, IncludeInfoLibItemLinkFile);

                using (IDataReader reader = database.ExecuteReader(command))
                {
                    if (reader.Read())
                    {
                        item = new InfoLibItem
                        {
                            InfoLibItemId = reader.GetInt32(0),
                            ParentId = reader.GetInt32(1),
                            ViewerScope = reader.IsDBNull(2) ? (Scope?)null : reader.GetInt16(2).ToEnumObject<Scope>(),
                            ItemHeader = new InfoLibItem.InfoLibItemHeader
                            {
                                HeaderText = reader.IsDBNull(3) ? string.Empty : reader.GetString(3),
                                HeaderType = reader.GetInt16(4).ToEnumObject<InfoLibHeaderType>(),
                                ItemFileMimeType = reader.IsDBNull(5) ? string.Empty : reader.GetString(5),
                                ItemFileExtension = reader.IsDBNull(6) ? string.Empty : reader.GetString(6),
                                ItemFileSizeInBytes = reader.IsDBNull(7) ? (int?)null : reader.GetInt32(7),
                                ItemFile = reader.IsDBNull(8) ? (byte[])null : (byte[])reader[8],
                                ItemFileName = reader.IsDBNull(9) ? string.Empty : reader.GetString(9),
                            },
                            CreatedBy = reader.GetInt32(18),
                            CreatedDate = reader.GetDateTime(19),
                            LastUpdatedBy = reader.IsDBNull(20) ? (int?)null : reader.GetInt32(20),
                            LastUpdatedDate = reader.IsDBNull(21) ? (DateTime?)null : reader.GetDateTime(21),
                            IsArchived = reader.GetBoolean(22),
                            SpecialIdentifier = reader.IsDBNull(23) ? (InfoLibSpecialIdentifiers?)null : reader.GetInt32(23).ToEnumObject<InfoLibSpecialIdentifiers>()

                        };
                        if (!reader.IsDBNull(10))
                        {
                            item.ItemLink = new InfoLibItem.InfoLibItemLink
                            {
                                LinkUrl = reader.IsDBNull(10) ? string.Empty : reader.GetString(10),
                                LinkType = reader.IsDBNull(11) ? (InfoLibLinkType?)null : reader.GetInt16(11).ToEnumObject<InfoLibLinkType>(),
                                ItemFileMimeType = reader.IsDBNull(12) ? string.Empty : reader.GetString(12),
                                ItemFileExtension = reader.IsDBNull(13) ? string.Empty : reader.GetString(13),
                                ItemFileSizeInBytes = reader.IsDBNull(14) ? (int?)null : reader.GetInt32(14),
                                OpenLinkInNewWindow = reader.GetBoolean(15),
                                ItemFile = reader.IsDBNull(16) ? (byte[])null : (byte[])reader[16],
                                ItemFileName = reader.IsDBNull(17) ? string.Empty : reader.GetString(17),
                            };
                        };
                    }

                    return item;
                }
            }
        }


        public InfoLibItem GetInfoLibTopLevelParent(int InfoLibItemId, bool IsArchived)
        {
            using (var command = database.GetStoredProcCommand(StoredProcNames.InfoLib.GetInfoLibTopLevelParent.Description()))
            {
                database.AddInParameter(command, "@InfoLibItemId", DbType.Int32, InfoLibItemId);
                database.AddInParameter(command, "@Archived", DbType.Boolean, IsArchived);

                InfoLibItem item = null;

                using (IDataReader reader = database.ExecuteReader(command))
                {
                    if (reader.Read())
                    {
                        item = new InfoLibItem
                        {
                            InfoLibItemId = reader.GetInt32(0),
                            ParentId = reader.GetInt32(1),
                            ItemHeader = new InfoLibItem.InfoLibItemHeader
                            {
                                HeaderText = reader.IsDBNull(2) ? string.Empty : reader.GetString(2),
                                HeaderType = reader.GetInt16(3).ToEnumObject<InfoLibHeaderType>()
                            },
                            IsArchived = reader.GetBoolean(7),
                            SpecialIdentifier = reader.IsDBNull(8) ? (InfoLibSpecialIdentifiers?)null : reader.GetInt32(8).ToEnumObject<InfoLibSpecialIdentifiers>()
                        };
                        if (!reader.IsDBNull(4))
                        {
                            item.ItemLink = new InfoLibItem.InfoLibItemLink
                            {
                                LinkType = reader.IsDBNull(4) ? (InfoLibLinkType?)null : reader.GetInt16(4).ToEnumObject<InfoLibLinkType>(),
                                LinkUrl = reader.IsDBNull(5) ? string.Empty : reader.GetString(5),
                                OpenLinkInNewWindow = reader.GetBoolean(6),
                            };
                        }
                    };
                }

                return item;
            }
        }

        public InfoLibItemFile GetInfoLibFile(int InfoLibItemId, InfoLibFileServerTypes type)
        {
            using (var command = database.GetStoredProcCommand(StoredProcNames.InfoLib.GetInfoLibItemFile.Description()))
            {

                InfoLibItemFile itemFile = null;

                database.AddInParameter(command, "@InfoLibItemId", DbType.Int32, InfoLibItemId);
                database.AddInParameter(command, "@FileTypeIdentifier", DbType.Int32, type.EnumValue<int>());

                using (IDataReader reader = database.ExecuteReader(command))
                {
                    if (reader.Read())
                    {
                        //Get file size
                        int? fileSizeinBytes = !reader.IsDBNull(2) ? reader.GetInt32(2) : (int?)null;

                        //Get file into the buffer
                        byte[] fileBuffer = null;
                        if (fileSizeinBytes.HasValue && fileSizeinBytes.Value > 0)
                        {
                            fileBuffer = new byte[fileSizeinBytes.Value];
                            int actualFileOrdinal = 3;
                            reader.GetBytes(actualFileOrdinal, 0, fileBuffer, 0, fileSizeinBytes.Value);
                        }
                        itemFile = new InfoLibItemFile()
                        {
                            ItemFileMimeType = !reader.IsDBNull(0) ? reader.GetString(0) : string.Empty,
                            ItemFileExtension = !reader.IsDBNull(1) ? reader.GetString(1) : string.Empty,
                            ItemFileSizeInBytes = fileSizeinBytes,
                            ItemFile = fileBuffer,
                            ItemFileName = !reader.IsDBNull(4) ? reader.GetString(4) : string.Empty,
                        };
                    }
                }

                return itemFile;
            }
        }



        public bool DeleteInfoLibItem(int InfoLibItemId, int? ScopeId)
        {
            using (var command = database.GetStoredProcCommand(StoredProcNames.InfoLib.DeleteInfoLibItem.Description()))
            {

                database.AddInParameter(command, "@ScopeId", DbType.Int32, ScopeId);
                database.AddInParameter(command, "@InfoLibItemId", DbType.Int32, InfoLibItemId);

                int RecAffected = database.ExecuteNonQuery(command);
                return RecAffected > 0;
            }
        }


        public IEnumerable<InfoLibItem> SearchInfoLibItems(Scope? ScopeId, string SearchText, bool IsArchived)
        {
            using (var command = database.GetStoredProcCommand(StoredProcNames.InfoLib.SearchInfoLibItems.Description()))
            {

                IList<InfoLibItem> items = null;

                database.AddInParameter(command, "@ScopeOfSearcher", DbType.Int32, ScopeId.HasValue ? ScopeId.Value.EnumValue<int>() : (int?)null);
                database.AddInParameter(command, "@SearchText", DbType.StringFixedLength, SearchText);
                database.AddInParameter(command, "@IsArchived", DbType.Boolean, IsArchived);

                InfoLibItem item = null;

                using (IDataReader reader = database.ExecuteReader(command))
                {
                    while (reader.Read())
                    {
                        if (items == null) items = new List<InfoLibItem>();
                        item = new InfoLibItem
                        {
                            InfoLibItemId = reader.GetInt32(0),
                            ParentId = reader.GetInt32(1),
                            ItemHeader = new InfoLibItem.InfoLibItemHeader
                            {
                                HeaderText = reader.IsDBNull(2) ? string.Empty : reader.GetString(2),
                                HeaderType = reader.GetInt16(3).ToEnumObject<InfoLibHeaderType>()
                            },
                            IsArchived = reader.GetBoolean(7),
                            SpecialIdentifier = reader.IsDBNull(8) ? (InfoLibSpecialIdentifiers?)null : reader.GetInt32(8).ToEnumObject<InfoLibSpecialIdentifiers>()
                        };
                        if (!reader.IsDBNull(4))
                        {
                            item.ItemLink = new InfoLibItem.InfoLibItemLink
                            {
                                LinkType = reader.IsDBNull(4) ? null : (InfoLibLinkType?)reader.GetInt16(4).ToEnumObject<InfoLibHeaderType>(),
                                LinkUrl = reader.IsDBNull(5) ? string.Empty : reader.GetString(5),
                                OpenLinkInNewWindow = reader.GetBoolean(6)
                            };
                        }

                        items.Add(item);
                    };
                }
                return items;
            }
        }


        //public IEnumerable<KeyValuePair<int, string>> GetSpecialIdentifiers()
        //{
        //    using (var command = database.GetStoredProcCommand(StoredProcNames.InfoLib.GetInfoLibSpecialIdentifiers.Description()))
        //    {
        //        using (IDataReader reader = database.ExecuteReader(command))
        //        {
        //            while (reader.Read())
        //                yield return new KeyValuePair<int, string>(reader.GetInt32(0), reader.GetString(1));
        //        }
        //    }
        //}

        public InfoLibForumCallItem GetInfoLibForumCallItem()
        {
            using (var command = database.GetStoredProcCommand(StoredProcNames.InfoLib.GetInfoLibForumCall.Description()))
            {
                using (IDataReader reader = database.ExecuteReader(command))
                {
                    InfoLibForumCallItem forumCallItem = null;
                    if (reader.Read())
                    {
                        forumCallItem = new InfoLibForumCallItem();
                        forumCallItem.SummaryItem = new InfoLibItem()
                        {
                            InfoLibItemId = reader.GetInt32(0),
                            ParentId = reader.GetInt32(1),
                            ItemHeader = new InfoLibItem.InfoLibItemHeader()
                            {
                                HeaderText = reader.IsDBNull(2) ? null : reader.GetString(2),
                                HeaderType = reader.GetInt16(3).ToEnumObject<InfoLibHeaderType>()
                            },
                            ViewerScope = reader.IsDBNull(4) ? (Scope?)null : reader.GetInt16(4).ToEnumObject<Scope>()
                        };

                        reader.NextResult();

                        if (reader.Read())
                        {
                            forumCallItem.DetailedItem = new InfoLibItem()
                            {
                                InfoLibItemId = reader.GetInt32(0),
                                ParentId = reader.GetInt32(1),
                                ItemHeader = new InfoLibItem.InfoLibItemHeader()
                                {
                                    HeaderText = reader.IsDBNull(2) ? null : reader.GetString(2),
                                    HeaderType = reader.GetInt16(3).ToEnumObject<InfoLibHeaderType>()
                                },
                                ViewerScope = reader.IsDBNull(4) ? (Scope?)null : reader.GetInt16(4).ToEnumObject<Scope>()
                            };
                        }
                    }
                    return forumCallItem;
                }
            }
        }

    }

}

