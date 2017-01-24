using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Coolbooru {
	/// <summary>
	/// Represents a search query.
	/// </summary>
	public class CoolSearch {
		/// <summary>
		/// The list of images (CoolItems) returned from Derpibooru.
		/// </summary>
		public List<CoolItem> search;
		/// <summary>
		/// The total number of images with the given tag.
		/// </summary>
		public int total;
		public List<object> interactions; // I literally do not know what this does. Sorry
	}

	/// <summary>
	/// Represents an item in the Derpibooru database.
	/// </summary>
	public class CoolItem {
		/// <summary>
		/// The Derpibooru ID of the image.
		/// </summary>
		public string id;
		/// <summary>
		/// The date the image was created.
		/// </summary>
		public DateTime created_at;
		/// <summary>
		/// The date the image was updated.
		/// </summary>
		public DateTime updated_at;
		public List<object> duplicate_reports; // not sure what this does either
		/// <summary>
		/// The date the image was first seen.
		/// </summary>
		public DateTime first_seen_at;
		/// <summary>
		/// The user ID of the user who uploaded the image.
		/// </summary>
		public string uploader_id;
		/// <summary>
		/// The net score of the image on Derpibooru.
		/// </summary>
		public int score;
		/// <summary>
		/// The number of comments on the image.
		/// </summary>
		public int comment_count;
		/// <summary>
		/// The image's width in pixels.
		/// </summary>
		public int width;
		/// <summary>
		/// The image's height in pixels.
		/// </summary>
		public int height;
		/// <summary>
		/// The name of the image file according to Derpibooru.
		/// </summary>
		public string file_name;
		/// <summary>
		/// The image's description on Derpibooru.
		/// </summary>
		public string description;
		/// <summary>
		/// The image uploader's username.
		/// </summary>
		public string uploader;
		/// <summary>
		/// The URI (without protocol) to the image page on Derpibooru.
		/// </summary>
		public string image;
		/// <summary>
		/// The number of upvotes on the image.
		/// </summary>
		public int upvotes;
		/// <summary>
		/// The number of downvotes on the image.
		/// </summary>
		public int downvotes;
		/// <summary>
		/// The number of users that favorited the image.
		/// </summary>
		public int faves;
		/// <summary>
		/// A comma-separated list of tags on the image.
		/// </summary>
		public string tags;
		/// <summary>
		/// A List of the IDs of all the tags on the image.
		/// </summary>
		public List<string> tag_ids;
		/// <summary>
		/// The image's computed aspect ratio.
		/// </summary>
		public double aspect_ratio;
		/// <summary>
		/// The original format of the image before it was uploaded to Derpibooru.
		/// </summary>
		public string original_format;
		/// <summary>
		/// The MIME type of the image.
		/// </summary>
		public string mime_type;
		/// <summary>
		/// The image file's SHA512 hash.
		/// </summary>
		public string sha512_hash;
		/// <summary>
		/// The image file's SHA512 hash before it was optimized by Derpibooru.
		/// </summary>
		public string orig_sha512_hash;
		/// <summary>
		/// The image's source URL, if provided.
		/// </summary>
		public string source_url;
		public CoolRepresentation representations;
		/// <summary>
		/// Whether or not the image was "rendered" by Derpibooru.
		/// </summary>
		public bool is_rendered;
		/// <summary>
		/// Whether or not the image was optimized by Derpibooru.
		/// </summary>
		public bool is_optimized;
	}

	/// <summary>
	/// Represents a "Representation" (e.g. images) of the item
	/// </summary>
	public class CoolRepresentation {
		/// <summary>
		/// A URL that points to a very small thumbnail for the image.
		/// </summary>
		public string thumb_tiny;
		/// <summary>
		/// A URL that points to a small thumbnail for the image.
		/// </summary>
		public string thumb_small;
		/// <summary>
		/// A URL that points to a thumbnail for the image.
		/// </summary>
		public string thumb;
		/// <summary>
		/// A URL that points to a small version of the image.
		/// </summary>
		public string small;
		/// <summary>
		/// A URL that points to a medium version of the image.
		/// </summary>
		public string medium;
		/// <summary>
		/// A URL that points to a full-resolution version of the image.
		/// </summary>
		public string full;
	}

	/// <summary>
	/// Represents the default Lists available.
	/// </summary>
	public class CoolLists {
		/// <summary>
		/// The default top scoring list.
		/// </summary>
		public List<CoolItem> top_scoring;
		/// <summary>
		/// The default top commented list.
		/// </summary>
		public List<CoolItem> top_commented;
		/// <summary>
		/// The list of all time top scoring images.
		/// </summary>
		public List<CoolItem> all_time_top_scoring;
		public List<object> interactions;
	}

	/// <summary>
	/// Represents the result of the Images API call.
	/// </summary>
	public class CoolImages {
		public List<CoolItem> images;
		public List<object> interactions;
	}

	/// <summary>
	/// Represents arguments to a search query.
	/// </summary>
	public class CoolSearchQuery {
		private string pQuery = "*";
		private string pSortFormat;

		/// <summary>
		/// Your Derpibooru API key.
		/// </summary>
		public string APIKey;
		/// <summary>
		/// The Derpibooru query to search for. Defaults to *, for all tags.
		/// </summary>
		public string Query {
			get { return pQuery; }
			set { pQuery = Uri.EscapeUriString(value); }
		}
		/// <summary>
		/// The sort format to sort the results by. See the SORT_* constants.
		/// </summary>
		public string SortFormat {
			get { return pSortFormat; }
			set { pSortFormat = Uri.EscapeUriString(value); }
		}
		/// <summary>
		/// The page of results to return. Defaults to 1.
		/// </summary>
		public int Page = 1;
		/// <summary>
		/// Whether or not we should include the comments. *NOTE* This is resouce-intensive for both you and Derpibooru. Consider fetching this information on a per-image basis.
		/// </summary>
		public bool IncludeComments = false;
		/// <summary>
		/// Whether or not we should include the favorited-by information. *NOTE* This is resouce-intensive for both you and Derpibooru. Consider fetching this information on a per-image basis.
		/// </summary>
		public bool IncludeFavoritedBy = false;
	}

	/// <summary>
	/// Represents arguments to a list query.
	/// </summary>
	public class CoolListQuery {
		/// <summary>
		/// Your Derpibooru API key.
		/// </summary>
		public string APIKey;
		/// <summary>
		/// The List to search for.
		/// </summary>
		public string List;
		/// <summary>
		/// The page of results to return. Defaults to 1.
		/// </summary>
		public int Page = 1;
		/// <summary>
		/// The sampling period to construct the lists from. Specified in hours (Xh), days (Xd), or weeks (Xw).
		/// </summary>
		public string SamplingPeriod;
		/// <summary>
		/// Whether or not we should include the comments. *NOTE* This is resouce-intensive for both you and Derpibooru. Consider fetching this information on a per-image basis.
		/// </summary>
		public bool IncludeComments = false;
		/// <summary>
		/// Whether or not we should include the favorited-by information. *NOTE* This is resouce-intensive for both you and Derpibooru. Consider fetching this information on a per-image basis.
		/// </summary>
		public bool IncludeFavoritedBy = false;
	}

	/// <summary>
	/// Represents a List.
	/// </summary>
	public class CoolList {
		/// <summary>
		/// The images in the list.
		/// </summary>
		public List<CoolItem> image;
		public List<object> interactions;
	}

	/// <summary>
	/// Represents a Gallery.
	/// </summary>
	public class CoolGallery {
		/// <summary>
		/// The date the gallery was created.
		/// </summary>
		public DateTime created_at;
		/// <summary>
		/// The date at which the gallery was updated.
		/// </summary>
		public DateTime updated_at;
		/// <summary>
		/// The ID of the user who created the gallery.
		/// </summary>
		public int creator_id;
		/// <summary>
		/// The gallery's description.
		/// </summary>
		public string description;
		/// <summary>
		/// The ID of the gallery.
		/// </summary>
		public int id;
		/// <summary>
		/// The number of images in the gallery.
		/// </summary>
		public int image_count;
		/// <summary>
		/// The spoiler warning set on the gallery.
		/// </summary>
		public string spoiler_warning;
		/// <summary>
		/// The gallery's title.
		/// </summary>
		public string title;
		/// <summary>
		/// The number of users watching the gallery.
		/// </summary>
		public int watcher_count;
	}

	/// <summary>
	/// Represents arguments to an front-page image list query.
	/// </summary>
	public class CoolImageQuery {
		private char? pOrder = null;

		/// <summary>
		/// Your Derpibooru API key.
		/// </summary>
		public string APIKey;
		/// <summary>
		/// The field to search and sort by. See the CONSTRAINT_* constants.
		/// </summary>
		/// <see cref="Coolbooru.CONSTRAINT_ID"/>
		/// <seealso cref="Coolbooru.CONSTRAINT_CREATED"/>
		/// <seealso cref="Coolbooru.CONSTRAINT_UPDATED"/>
		public string Constraint;
		/// <summary>
		/// The page of results to return. Defaults to 1.
		/// </summary>
		public int Page = 1;
		/// <summary>
		/// When specified, states that the ID must be greater than the given value. Use in conjunction with the id constraint.
		/// </summary>
		public int? IDGreaterThan;
		/// <summary>
		/// When specified, states that the ID must be greater than or equal to the given value. Use in conjunction with the id constraint.
		/// </summary>
		public int? IDGreaterThanOrEqualTo;
		/// <summary>
		/// When specified, states that the ID must be less than the given value. Use in conjunction with the id constraint.
		/// </summary>
		public int? IDLessThan;
		/// <summary>
		/// When specified, states that the ID must be less than or equal to the given value. Use in conjunction with the id constraint.
		/// </summary>
		public int? IDLessThanOrEqualTo;
		/// <summary>
		/// When specified, states that the time must be greater than the given value. Use in conjunction with the created_at and updated_at constraints.
		/// </summary>
		public string TimeGreaterThan;
		/// <summary>
		/// When specified, states that the time must be greater than or equal to the given value. Use in conjunction with the created_at and updated_at constraints.
		/// </summary>
		public string TimeGreaterThanOrEqualTo;
		/// <summary>
		/// When specified, states that the time must be less than the given value. Use in conjunction with the created_at and updated_at constraints.
		/// </summary>
		public string TimeLessThan;
		/// <summary>
		/// When specified, states that the time must be less than or equal to the given value. Use in conjunction with the created_at and updated_at constraints.
		/// </summary>
		public string TimeLessThanOrEqualTo;
		/// <summary>
		/// The way to sort the images (a for ascending, d for descending).
		/// </summary>
		public char? Order {
			get {
				return pOrder;
			}
			set {
				if( value != 'a' && value != 'd' && value != null) {
					throw new ArgumentException("Must be 'a' for ascending or 'd' for descending, or null");
				}
				pOrder = value;
			}
		}
		/// <summary>
		/// Whether or not to include information about duplicate or deleted images in the results. Metadata is limited to id, created_at, updated_at, and either deletion_reason or duplicate_of.
		/// </summary>
		public bool Deleted;
		/// <summary>
		/// Whether or not we should include the comments. *NOTE* This is resouce-intensive for both you and Derpibooru. Consider fetching this information on a per-image basis.
		/// </summary>
		public bool IncludeComments = false;
		/// <summary>
		/// Whether or not we should include the favorited-by information. *NOTE* This is resouce-intensive for both you and Derpibooru. Consider fetching this information on a per-image basis.
		/// </summary>
		public bool IncludeFavoritedBy = false;
		/// <summary>
		/// When specified, sorts the images randomly.
		/// </summary>
		public bool SortRandomly;
	}

	/// <summary>
	/// Represents arguments to a user gallery query.
	/// </summary>
	public class CoolUserGalleryQuery {
		/// <summary>
		/// Your Derpibooru API key.
		/// </summary>
		public string APIKey;
		/// <summary>
		/// THe username of the user to pull galleries from.
		/// </summary>
		public string User;
		/// <summary>
		/// The ID of the gallery to pull. Optional.
		/// </summary>
		public int? ID;
		/// <summary>
		/// The page of results to return. Defaults to 1.
		/// </summary>
		public int Page = 1;
		/// <summary>
		/// Whether or not to return an array of image IDs featured in each gallery, in the order defined by the owner. Disregards content filters.
		/// </summary>
		public bool IncludeImages = false;
	}

	/// <summary>
	/// Represents an oEmbed response.
	/// </summary>
	public class CoolEmbed {
		/// <summary>
		/// The version of oEmbed used.
		/// </summary>
		public string version;
		/// <summary>
		/// The type of oEmbed response.
		/// </summary>
		public string type;
		/// <summary>
		/// The title parameter of the oEmbed structure.
		/// </summary>
		public string title;
		/// <summary>
		/// The author's URL.
		/// </summary>
		public string author_url;
		/// <summary>
		/// The author's name (specified by artist: tag).
		/// </summary>
		public string author_name;
		/// <summary>
		/// The provider name (usually Derpibooru).
		/// </summary>
		public string provider_name;
		/// <summary>
		/// The URL of the image page on Derpibooru.
		/// </summary>
		public string provider_url;
		/// <summary>
		/// A Unix timestamp that specifies an age for caching purposes. Derpibooru recommends you cache your oEmbed responses!
		/// </summary>
		public int cache_age;
		/// <summary>
		/// The Derpibooru ID of the image.
		/// </summary>
		public int derpibooru_id;
		/// <summary>
		/// The net score on Derpibooru for the image.
		/// </summary>
		public int derpibooru_score;
		/// <summary>
		/// The number of comments on the image.
		/// </summary>
		public int derpibooru_comments;
		/// <summary>
		/// A list of tags applied to the image.
		/// </summary>
		public List<string> derpibooru_tags;
		/// <summary>
		/// The direct link to the image.
		/// </summary>
		public string thumbnail_url;
	}

	/// <summary>
	/// The main functions of Coolbooru.
	/// </summary>
	public class CoolRequests {
		/// <summary>
		/// The ID constraint for use with List searches.
		/// </summary>
		public const string CONSTRAINT_ID = "id";
		/// <summary>
		/// The updated_at constraint to used with List searches.
		/// </summary>
		public const string CONSTRAINT_UPDATED = "updated";
		/// <summary>
		/// The created_at constraint to be used with List searches.
		/// </summary>
		public const string CONSTRAINT_CREATED = "created";

		/// <summary>
		/// Sort by the time the image was uploaded.
		/// </summary>
		public const string SORT_CREATEDAT = "created_at";
		/// <summary>
		/// Sort by the image's score.
		/// </summary>
		public const string SORT_SCORE = "score";
		/// <summary>
		/// Sort by the Derpibooru-determined "relevance" to your query.
		/// </summary>
		public const string SORT_RELEVANCE = "relevance";
		/// <summary>
		/// Sort by image width.
		/// </summary>
		public const string SORT_WIDTH = "width";
		/// <summary>
		/// Sort by image height.
		/// </summary>
		public const string SORT_HEIGHT = "height";
		/// <summary>
		/// Sort randomly!
		/// </summary>
		public const string SORT_RANDOM = "random";

		/// <summary>
		/// Creates a new HTTP client for use with API calls.
		/// </summary>
		/// <returns>a new HTTP client for use with API calls.</returns>
		private static HttpClient ClientFactory() {
			HttpClient c = new HttpClient();
			c.DefaultRequestHeaders.UserAgent.ParseAdd("Coolbooru");
			return c;
		}

		/// <summary>
		/// Complete a response at url using the model T.
		/// </summary>
		/// <typeparam name="T">Model to serialize to.</typeparam>
		/// <param name="url">URL to query.</param>
		/// <returns>Response based on the model passed in.</returns>
		private static async Task<T> DoResponse<T>(string url) {
			return JsonConvert.DeserializeObject<T>(await ClientFactory().GetStringAsync(url));
		}

		/// <summary>
		/// Search Derpibooru using a certain tag or tags.
		/// See https://derpibooru.org/search/syntax for search syntax information.
		/// </summary>
		/// <param name="query">The tag(s) to search for (e.g. pinkie pie)</param>
		/// <param name="page">Pagination</param>
		/// <returns>A CoolSearch representing the result</returns>
		public static async Task<CoolSearch> Search(string query, int page = 1) {
			return await DoResponse<CoolSearch>("https://derpibooru.org/search.json?q=" + Uri.EscapeUriString(query) + "&page=" + page);
		}

		/// <summary>
		/// Search Derpibooru using a CoolSearchQuery.
		/// </summary>
		/// <param name="q">A CoolSearchQuery object</param>
		/// <returns>A CoolSearch representing the result</returns>
		public static async Task<CoolSearch> Search(CoolSearchQuery q) {
			string url = "https://derpibooru.org/search.json?q=" + q.Query + "&page=" + q.Page;
			if (q.APIKey != null) url += "&key=" + q.APIKey;
			if (q.IncludeComments) url += "&comments=true";
			if (q.IncludeFavoritedBy) url += "&fav=true";
			if (q.SortFormat != null) url += "&sf=" + q.SortFormat;

			return await DoResponse<CoolSearch>(url);
		}

		/// <summary>
		/// Get information on a certain item
		/// </summary>
		/// <param name="itemID">An item ID.</param>
		/// <returns>A CoolItem representing the result</returns>
		public static async Task<CoolItem> Item(int itemID) {
			return await DoResponse<CoolItem>("https://derpibooru.org/" + itemID + ".json");
		}

		/// <summary>
		/// Get the contents of lists top_scoring, top_commented, all_time_top_scoring
		/// </summary>
		/// <returns>A CoolLists object representing the result</returns>
		public static async Task<CoolLists> Lists() {
			return await DoResponse<CoolLists>("https://derpibooru.org/lists.json");
		}

		/// <summary>
		/// Get the contents of a specific list.
		/// </summary>
		/// <param name="list">The list to get the contents of.</param>
		/// <returns>A CoolList object representing the result</returns>
		public static async Task<CoolList> List(string list, int page = 1) {
			return await DoResponse<CoolList>("https://derpibooru.org/lists/" + list + ".json&page=" + page);
		}


		/// <summary>
		/// Get the contents of a specific list.
		/// </summary>
		/// <param name="q">A CoolListQuery representing the parameters of the request.</param>
		/// <returns>A CoolList object representing the result</returns>
		public static async Task<CoolList> List(CoolListQuery q) {
			string url = "https://derpibooru.org/lists/" + q.List + ".json?page=" + q.Page;
			if (q.APIKey != null) url += "&key=" + q.APIKey;
			if (q.IncludeComments) url += "&comments=true";
			if (q.IncludeFavoritedBy) url += "&fav=true";

			return await DoResponse<CoolList>(url);
		}

		/// <summary>
		/// Get a user's galleries.
		/// </summary>
		/// <param name="user">The user whose galleries you want to get.</param>
		/// <returns>A List of CoolGalleries representing the result</returns>
		public static async Task<List<CoolGallery>> UserGalleries(string user) {
			return await DoResponse<List<CoolGallery>>("https://derpibooru.org/galleries/" + user + ".json");
		}

		/// <summary>
		/// Get a user's galleries.
		/// </summary>
		/// <param name="user">The user whose galleries you want to get.</param>
		/// <param name="page">Pagination.</param>
		/// <param name="include_images">"When set, include arrays of image IDs featured in each gallery, in the order defined by the owning user, disregarding content filters." Doesn't seem to do much, though.</param>
		/// <returns>A List of CoolGalleries representing the result</returns>
		public static async Task<List<CoolGallery>> UserGalleries(string user, int page = 1, bool include_images = false) {
			string url = "https://derpibooru.org/galleries/" + user + ".json?page=" + page;
			if (include_images) url += "&include_images=true";

			return await DoResponse<List<CoolGallery>>(url);
		}

		/// <summary>
		/// Get a gallery.
		/// </summary>
		/// <param name="user">The user who owns the gallery.</param>
		/// <param name="id">The gallery ID.</param>
		/// <param name="page">Pagination.</param>
		/// <returns>A CoolGallery representing the result.</returns>
		public static async Task<CoolGallery> UserGallery(string user, int id, int page = 1) {
			string url = "https://derpibooru.org/galleries/" + user + "/" + id + ".json?page=" + page;
			return await DoResponse<CoolGallery>(url);
		}

		/// <summary>
		/// Get a gallery.
		/// </summary>
		/// <param name="id">The gallery ID.</param>
		/// <param name="page">Pagination.</param>
		/// <returns>A CoolGallery representing the result.</returns>
		public static async Task<CoolGallery> UserGallery(int id, int page = 1) {
			return await DoResponse<CoolGallery>("https://derpibooru.org/galleries/" + id + ".json?page=" + page);
		}

		/// <summary>
		/// Get a gallery with a CoolUserGalleryQuery.
		/// </summary>
		/// <param name="q">A CoolUserGalleryQuery object</param>
		/// <returns>A CoolGallery representing the result.</returns>
		public static async Task<CoolGallery> UserGallery(CoolUserGalleryQuery q) {
			string url = "https://derpibooru.org/galleries/";
			if (q.User != null && q.ID != null)
				url += q.User + "/" + q.ID + ".json";
			else {
				if (q.User != null) url += q.User + ".json";
				if (q.ID != null) url += q.ID + ".json";
			}

			url += "?page=" + q.Page;
			if (q.IncludeImages) url += "&include_images=true";
			if (q.APIKey != null) url += "&key=" + q.APIKey;

			return await DoResponse<CoolGallery>(url);
		}

		/// <summary>
		/// Get the front-page images.
		/// </summary>
		/// <returns>A CoolImages object representing the result</returns>
		public static async Task<CoolImages> Images() {
			return await DoResponse<CoolImages>("https://derpibooru.org/images.json");
		}

		/// <summary>
		/// Filter the front-page images.
		/// </summary>
		/// <param name="q">A CoolImageQuery representing the query.</param>
		/// <returns>A CoolImages object representing the result.</returns>
		public static async Task<CoolImages> Images(CoolImageQuery q) {
			string url = "https://derpibooru.org/images.json?page=" + q.page;
			if (q.APIKey != null) url += "&key=" + q.key;
			if (q.Constraint != null) {
				if (q.Constraint == CONSTRAINT_ID) {
					if (q.IDGreaterThan != null)
						url += "&gt=" + q.IDGreaterThan;
					else if (q.IDGreaterThanOrEqualTo != null)
						url += "&gte=" + q.IDGreaterThanOrEqualTo;
					else if (q.IDLessThan != null)
						url += "&lt=" + q.IDLessThan;
					else if (q.IDLessThanOrEqualTo != null)
						url += "&lte=" + q.IDLessThanOrEqualTo;
				} else if (q.Constraint == CONSTRAINT_CREATED || q.Constraint == CONSTRAINT_UPDATED) {
					if (q.TimeGreaterThan != null)
						url += "&gt=" + q.TimeGreaterThan;
					else if (q.TimeGreaterThanOrEqualTo != null)
						url += "&gte=" + q.TimeGreaterThanOrEqualTo;
					else if (q.TimeLessThan != null)
						url += "&lt=" + q.TimeLessThan;
					else if (q.TimeLessThanOrEqualTo != null)
						url += "&lte=" + q.TimeLessThanOrEqualTo;
				}
			}
			if (q.Order != null) url += "&order=" + q.Order;
			if (q.Deleted) url += "&deleted=true";
			if (q.IncludeComments) url += "&comments=true";
			if (q.IncludeFavoritedBy) url += "&fav=true";
			if (q.SortRandomly) url += "&random_image=true";

			return await DoResponse<CoolImages>(url);
		}

		/// <summary>
		/// Get OEmbed information from a post ID.
		/// </summary>
		/// <param name="id">A post ID.</param>
		/// <returns>A CoolEmbed object representing the result.</returns>
		public static async Task<CoolEmbed> Embed(int id) {
			return await DoResponse<CoolEmbed>("https://derpibooru.org/oembed.json?url=https://derpibooru.org/" + id);
		}

		/// <summary>
		/// Get OEmbed information from a URL.
		/// </summary>
		/// <param name="url">A URL.</param>
		/// <returns>A CoolEmbed object representing the result.</returns>
		public static async Task<CoolEmbed> Embed(string url) {
			return await DoResponse<CoolEmbed>("https://derpibooru.org/oembed.json?url=" + url);
		}
	}
}
